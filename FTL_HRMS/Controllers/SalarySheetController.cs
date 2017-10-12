using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class SalarySheetController : Controller
    { 
        // GET: SalarySheet
        private HRMSDbContext _db = new HRMSDbContext();
        AttendanceController att = new AttendanceController();

        public ActionResult Index()
        {
            int LastPaidSalaryDurationId = 0;
            if (_db.PaidSalaryDuration.ToList().Count > 0)
            {
                LastPaidSalaryDurationId = _db.PaidSalaryDuration.Max(i => i.Sl);
            }            
            List<MonthlySalarySheet> SalarySheet = new List<MonthlySalarySheet>();
            if (LastPaidSalaryDurationId > 0)
            {
                SalarySheet = _db.MonthlySalarySheet.Include(i=> i.Employee).Include(i=> i.PaidSalaryDuration).Where(i => i.PaidSalaryDurationId == LastPaidSalaryDurationId).ToList();
                DateTime FromDate = _db.PaidSalaryDuration.Where(i => i.Sl == LastPaidSalaryDurationId).Select(i => i.FromDate).FirstOrDefault();
                DateTime ToDate = _db.PaidSalaryDuration.Where(i => i.Sl == LastPaidSalaryDurationId).Select(i => i.ToDate).FirstOrDefault();
                ViewBag.FromDate = FromDate.ToShortDateString();
                ViewBag.ToDate = ToDate.ToShortDateString();
            }
            return View(SalarySheet);
        }

        public ActionResult CalculateSalarySheet(DateTime StartDate, DateTime EndDate)
        {
            DateTime ToDate = Utility.Utility.GetDefaultDate();
            int LastPaidSalaryDurationId = 0;
            if (_db.PaidSalaryDuration.ToList().Count > 0)
            {
                LastPaidSalaryDurationId = _db.PaidSalaryDuration.Max(i => i.Sl);
            }
            if (LastPaidSalaryDurationId > 0)
            {
                ToDate = _db.PaidSalaryDuration.Where(i => i.Sl == LastPaidSalaryDurationId).Select(i => i.ToDate).FirstOrDefault();
            }
            if(StartDate.Date > ToDate.Date && EndDate.Date < DateTime.Now.Date)
            {
                att.SyncAttendance();
                List<int> EmployeeSlList = GetEmployeeSlFromMonthlyAttendance(StartDate, EndDate);
                double WorkingDays = GetWorkingDays(StartDate);
                int PaidSalaryDurationId = InsertPaidSalaryDuration(StartDate, EndDate, WorkingDays);

                if (PaidSalaryDurationId > 0)
                {
                    foreach (var sl in EmployeeSlList)
                    {
                        double GrossSalary = GetGrossSalary(sl);
                        double BasicSalary = GetBasicSalary(sl);
                        double PerDaySalary = GetPerDaySalary(GrossSalary, WorkingDays);
                        double DaysOfSalary = GetDaysOfSalary(StartDate, EndDate);
                        double EmployeeGrossSalary = GetEmployeeGrossSalary(PerDaySalary, DaysOfSalary);
                        double AbsentDays = GetAbsentDays(sl, StartDate, EndDate);
                        double AbsentPanelty = GetAbsentPanelty(AbsentDays, PerDaySalary);
                        double LateDays = GetLateDays(sl, StartDate, EndDate);
                        double WithoutPayLeave = GetWithoutPayLeave(sl);

                        if (LateDays > 0)
                        {
                            int LeavePaneltyDays = 0;
                            int BranchId = GetEmployeeBranchId(sl);
                            double LateConsiderationDays = (double)GetLateConsiderationDays(BranchId);
                            if (LateConsiderationDays > 0)
                            {
                                LeavePaneltyDays = GetLeavePaneltyDays(LateDays, LateConsiderationDays);
                            }
                            double EarnLeave = GetEarnLeave(sl);
                            if (CalculateLeavePanelty(sl, LeavePaneltyDays, EarnLeave, WithoutPayLeave))
                            {
                                EmployeeLeaveCountHistory empLeaveCount = new EmployeeLeaveCountHistory();
                                empLeaveCount.EmployeeId = sl;
                                empLeaveCount.PaidSalaryDurationId = PaidSalaryDurationId;
                                if (EarnLeave > LeavePaneltyDays)
                                {
                                    empLeaveCount.EarnLeaveDays = LeavePaneltyDays;
                                }
                                else
                                {
                                    empLeaveCount.EarnLeaveDays = EarnLeave;
                                }
                                empLeaveCount.WithoutPayLeaveDays = WithoutPayLeave;
                                _db.EmployeeLeaveCountHistory.Add(empLeaveCount);
                                _db.SaveChanges();
                                //leave counts updated
                            }
                            else
                            {
                                //leave counts calculation failed
                            }
                        }

                        double LatePanelty = 0;
                        double LeavePanelty = GetLeavePanelty(sl, PerDaySalary, WithoutPayLeave);
                        double UnofficialDays = GetUnofficialDays(sl, StartDate, EndDate);
                        double UnofficialPanelty = GetUnofficialPanelty(PerDaySalary, UnofficialDays);

                        //double BeforeJoiningDays = GetBeforeJoiningDays(sl, StartDate, EndDate);
                        //double UnofficialPanelty = 0;
                        //if (BeforeJoiningDays > 0)
                        //{
                        //    UnofficialPanelty += GetUnofficialPanelty(PerDaySalary, BeforeJoiningDays);
                        //}
                        //double AfterResignDays = GetAfterResignDays(sl, StartDate, EndDate);
                        //if (AfterResignDays > 0)
                        //{
                        //    UnofficialPanelty += GetUnofficialPanelty(PerDaySalary, AfterResignDays);
                        //}

                        double OthersPanelty = GetOthersPanelty(sl, StartDate, EndDate);
                        double OthersBonus = GetOthersBonus(sl, StartDate, EndDate);
                        double FestivalBonus = GetFestivalBonus(sl, StartDate, EndDate);
                        double AdjustmentAmount = GetAdjustmentAmount(sl, StartDate, EndDate);

                        MonthlySalarySheet monthlySalarySheet = new MonthlySalarySheet();
                        monthlySalarySheet.EmployeeId = sl;
                        monthlySalarySheet.PaidSalaryDurationId = PaidSalaryDurationId;
                        monthlySalarySheet.GrossSalary = GrossSalary;
                        monthlySalarySheet.BasicSalary = BasicSalary;
                        monthlySalarySheet.AbsentDay = AbsentDays;
                        monthlySalarySheet.AbsentPanelty = AbsentPanelty;
                        monthlySalarySheet.LateDay = LateDays;
                        monthlySalarySheet.LatePenalty = LatePanelty;
                        monthlySalarySheet.UnofficialDay = UnofficialDays;
                        monthlySalarySheet.UnofficialPenalty = UnofficialPanelty;
                        monthlySalarySheet.LeavePenalty = LeavePanelty;
                        monthlySalarySheet.OthersPenalty = OthersPanelty;
                        monthlySalarySheet.OthersBonus = OthersBonus;
                        monthlySalarySheet.FestivalBonus = FestivalBonus;
                        monthlySalarySheet.AdjustmentAmount = AdjustmentAmount;
                        monthlySalarySheet.NetPay = CalculateNetPay(EmployeeGrossSalary, AbsentPanelty, LatePanelty, UnofficialPanelty, LeavePanelty, OthersPanelty, OthersBonus, FestivalBonus, AdjustmentAmount);
                        _db.MonthlySalarySheet.Add(monthlySalarySheet);
                        _db.SaveChanges();
                    }
                    UpdateMonthlyAttendanceStatus(StartDate, EndDate);
                }
                else
                {
                    //Failed to insert paid salary duration
                }
            }
            else
            {
                //Wrong Input
            }
            return RedirectToAction("Index");
        }

        public List<int> GetEmployeeSlFromMonthlyAttendance(DateTime StartDate, DateTime EndDate)
        {
            return _db.MonthlyAttendance.Where(i => i.IsCalculated == false && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList().Select(m => m.EmployeeId).Distinct().ToList();
        }

        public double GetWorkingDays(DateTime StartDate)
        {
            return System.DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
        }

        public double GetDaysOfSalary(DateTime StartDate, DateTime EndDate)
        {
            return (EndDate - StartDate).TotalDays + 1;
        }

        public int InsertPaidSalaryDuration(DateTime StartDate, DateTime EndDate, double WorkingDays)
        {
            int sl = 0;
            try
            {
                PaidSalaryDuration paidSalaryDuration = new PaidSalaryDuration();
                paidSalaryDuration.FromDate = StartDate;
                paidSalaryDuration.ToDate = EndDate;
                paidSalaryDuration.WorkingDay = WorkingDays;
                _db.PaidSalaryDuration.Add(paidSalaryDuration);
                _db.SaveChanges();
                sl = paidSalaryDuration.Sl;
            }
            catch
            {
                return sl;
            }
            return sl;
        }

        public double GetGrossSalary(int sl)
        {
            return _db.EmployeeSalaryDistribution.Where(i => i.EmployeeId == sl).Select(i => i.GrossSalary).FirstOrDefault();
        }

        public double GetBasicSalary(int sl)
        {
            return _db.EmployeeSalaryDistribution.Where(i => i.EmployeeId == sl).Select(i => i.BasicSalary).FirstOrDefault();
        }

        public double GetPerDaySalary(double GrossSalary, double WorkingDays)
        {
            double PerDaySalary = 0;
            if(GrossSalary > 0 && WorkingDays > 0)
            {
                PerDaySalary = GrossSalary / WorkingDays;
            }
            return PerDaySalary;
        }

        public double GetEmployeeGrossSalary(double PerDaySalary, double DaysOfSalary)
        {
            double EmployeeGrossSalary = 0;
            if (PerDaySalary > 0 && DaysOfSalary > 0)
            {
                EmployeeGrossSalary = DaysOfSalary * PerDaySalary;
            }
            return EmployeeGrossSalary;
        }

        public double GetAbsentDays(int sl, DateTime StartDate, DateTime EndDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date && i.Status == "A" && i.IsCalculated == false).ToList().Count;
        }

        public double GetAbsentPanelty(double AbsentDays, double PerDaySalary)
        {
            double AbsentPanelty = 0;
            if(AbsentDays > 0 && PerDaySalary > 0)
            {
                AbsentPanelty = AbsentDays * PerDaySalary;
            }
            return AbsentPanelty;
        }

        public double GetLateDays(int sl, DateTime StartDate, DateTime EndDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date && i.Status == "L" && i.IsCalculated == false).ToList().Count;
        }

        public int GetEmployeeBranchId(int sl)
        {
            return _db.Employee.Where(i => i.Sl == sl).Select(i => i.BranchId).FirstOrDefault();
        }

        public double? GetLateConsiderationDays(int BranchId)
        {
            return _db.Branches.Where(i => i.Sl == BranchId).Select(i => i.LateConsiderationDay).FirstOrDefault();
        }

        public int GetLeavePaneltyDays(double LateDays, double LateConsiderationDays)
        {
            int LeavePanelty = 0;
            if(LateDays > 0 && LateConsiderationDays > 0)
            {
                LeavePanelty = Convert.ToInt32(LateDays / LateConsiderationDays);
            }
            return LeavePanelty;
        }

        public double GetEarnLeave(int sl)
        {
            return _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Earn").Select(i => i.AvailableDay).FirstOrDefault();
        }

        public double GetWithoutPayLeave(int sl)
        {
            return _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Without Pay").Select(i => i.AvailableDay).FirstOrDefault();
        }

        public bool CalculateLeavePanelty(int sl, int LeavePaneltyDays, double EarnLeave, double WithoutPayLeave)
        {
            try
            {
                double result = EarnLeave - LeavePaneltyDays;
                if (result < 0)
                {
                    int earnLeaveId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Earn").Select(i => i.Sl).FirstOrDefault();
                    var earnLeave = _db.LeaveCounts.Find(earnLeaveId);
                    earnLeave.AvailableDay = 0;
                    _db.Entry(earnLeave).State = EntityState.Modified;
                    _db.SaveChanges();

                    int withoutPayLeaveId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Without Pay").Select(i => i.Sl).FirstOrDefault();
                    var withoutPayLeave = _db.LeaveCounts.Find(withoutPayLeaveId);
                    withoutPayLeave.AvailableDay = withoutPayLeave.AvailableDay - result;
                    _db.Entry(withoutPayLeave).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    int earnLeaveId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Earn").Select(i => i.Sl).FirstOrDefault();
                    var earnLeave = _db.LeaveCounts.Find(earnLeaveId);
                    earnLeave.AvailableDay = result;
                    _db.Entry(earnLeave).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double GetLeavePanelty(int sl, double PerDaySalary, double WithoutPayLeave)
        {
            double LeavePanelty = 0;
            if(PerDaySalary > 0 && WithoutPayLeave > 0)
            {
                LeavePanelty = PerDaySalary * WithoutPayLeave;

                int withoutPayLeaveId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Without Pay").Select(i => i.Sl).FirstOrDefault();
                var withoutPayLeave = _db.LeaveCounts.Find(withoutPayLeaveId);
                withoutPayLeave.AvailableDay = 0;
                _db.Entry(withoutPayLeave).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                LeavePanelty = 0;
            }
            return LeavePanelty;
        }

        public double GetUnofficialDays(int sl, DateTime StartDate, DateTime EndDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date && i.Status == "U" && i.IsCalculated == false).ToList().Count;
        }

        public DateTime GetJoiningDate(int sl)
        {
            return _db.Employee.Where(i => i.Sl == sl).Select(i => i.DateOfJoining).FirstOrDefault();
        }

        public bool CheckJoiningDateContains(DateTime StartDate, DateTime EndDate, DateTime JoiningDate)
        {
            if(JoiningDate.Date >= StartDate.Date && JoiningDate.Date <= EndDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CalculateBeforeJoiningDays(DateTime StartDate, DateTime JoiningDate)
        {
            double BeforeJoiningDays = 0;
            if(JoiningDate.Date > StartDate.Date)
            {
                BeforeJoiningDays = JoiningDate.Day - StartDate.Day;
            }
            else
            {
                BeforeJoiningDays = 0;
            }
            return BeforeJoiningDays;
        }

        public double GetBeforeJoiningDays(int sl, DateTime StartDate, DateTime EndDate)
        {
            double BeforeJoiningDays = 0;
            DateTime JoiningDate = GetJoiningDate(sl);
            if(CheckJoiningDateContains(StartDate, EndDate, JoiningDate))
            {
                BeforeJoiningDays = CalculateBeforeJoiningDays(StartDate, JoiningDate);
            }
            else
            {
                BeforeJoiningDays = 0;
            }
            return BeforeJoiningDays;
        }

        public DateTime GetResignDate(int sl)
        {
            return _db.Resignation.Where(i => i.EmployeeId == sl && i.Status == "Approved").Select(i => i.ResignDate).FirstOrDefault();
        }

        public bool CheckResignDateContains(DateTime StartDate, DateTime EndDate, DateTime ResignDate)
        {
            if (ResignDate.Date >= StartDate.Date && ResignDate.Date <= EndDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CalculateAfterResignDays(DateTime EndDate, DateTime ResignDate)
        {
            double AfterResignDays = 0;
            if (ResignDate.Date < EndDate.Date)
            {
                AfterResignDays = EndDate.Day - ResignDate.Day;
            }
            else
            {
                AfterResignDays = 0;
            }
            return AfterResignDays;
        }

        public double GetAfterResignDays(int sl, DateTime StartDate, DateTime EndDate)
        {
            double AfterResignDays = 0;
            DateTime ResignDate = GetResignDate(sl);
            if (CheckResignDateContains(StartDate, EndDate, ResignDate))
            {
                AfterResignDays = CalculateAfterResignDays(EndDate, ResignDate);
            }
            else
            {
                AfterResignDays = 0;
            }
            return AfterResignDays;
        }

        public double GetUnofficialPanelty(double PerDaySalary, double UnofficialDays)
        {
            double UnofficialPanelty = 0;
            if(PerDaySalary > 0 && UnofficialDays > 0)
            {
                UnofficialPanelty = PerDaySalary * UnofficialDays;
            }
            else
            {
                UnofficialPanelty = 0;
            }
            return UnofficialPanelty;
        }

        public double GetOthersPanelty(int sl, DateTime StartDate, DateTime EndDate)
        {
            if(_db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Penalty" && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList().Count > 0)
            {
                return _db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Penalty" && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).Sum(i => i.Amount);
            }
            else
            {
                return 0;
            }            
        }

        public double GetOthersBonus(int sl, DateTime StartDate, DateTime EndDate)
        {
            if(_db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Bonus" && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList().Count > 0)
            {
                return _db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Bonus" && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).Sum(i => i.Amount);
            }
            else
            {
                return 0;
            }
        }

        public double GetFestivalBonus(int sl, DateTime StartDate, DateTime EndDate)
        {
            double FestivalBonus = 0;
            List<FestivalBonus> FestivalBonusList =  _db.FestivalBonus.Where(i => DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList();
            if(FestivalBonusList.Count > 0)
            {
                foreach(var item in FestivalBonusList)
                {
                    if(item.BasedOn == "Gross")
                    {
                        double GrossSalary = GetGrossSalary(sl);
                        FestivalBonus = FestivalBonus + GrossSalary * item.BonusPersentage / 100;
                    }
                    else if(item.BasedOn == "Basic")
                    {
                        double BasicSalary = GetBasicSalary(sl);
                        FestivalBonus = FestivalBonus + BasicSalary * item.BonusPersentage / 100;
                    }
                    else
                    {
                        FestivalBonus = 0;
                    }
                }
            }
            return FestivalBonus;
        }

        public double GetAdjustmentAmount(int sl, DateTime StartDate, DateTime EndDate)
        {
            if (_db.SalaryAdjustment.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList().Count > 0)
            {
                return _db.SalaryAdjustment.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).Sum(i => i.Amount);
            }
            else
            {
                return 0;
            }
        }

        public double CalculateNetPay(double EmployeeGrossSalary, double AbsentPanelty, double LatePanelty,double UnofficialPanelty,double LeavePanelty,double OthersPanelty,double OthersBonus,double FestivalBonus, double AdjustmentAmount)
        {
            return EmployeeGrossSalary - AbsentPanelty - LatePanelty - UnofficialPanelty - LeavePanelty - OthersPanelty + OthersBonus + FestivalBonus + AdjustmentAmount;
        }

        public List<MonthlyAttendance> GetMonthlyAttendance(DateTime StartDate, DateTime EndDate)
        {
            return _db.MonthlyAttendance.Where(i => DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList();
        }

        public bool UpdateMonthlyAttendanceStatus(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                List<MonthlyAttendance> EmployeeAttendance = GetMonthlyAttendance(StartDate, EndDate);
                if (EmployeeAttendance.Count > 0)
                {
                    EmployeeAttendance.ForEach(x => x.IsCalculated = true);
                }
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
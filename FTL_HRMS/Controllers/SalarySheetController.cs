using FTL_HRMS.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class SalarySheetController : Controller
    { 
        // GET: SalarySheet
        private HRMSDbContext _db = new HRMSDbContext();
        AttendanceController _att = new AttendanceController();

        public ActionResult Index()
        {
            int lastPaidSalaryDurationId = 0;
            if (_db.PaidSalaryDuration.ToList().Count > 0)
            {
                lastPaidSalaryDurationId = _db.PaidSalaryDuration.Max(i => i.Sl);
            }            
            List<MonthlySalarySheet> salarySheet = new List<MonthlySalarySheet>();
            if (lastPaidSalaryDurationId > 0)
            {
                salarySheet = _db.MonthlySalarySheet.Include(i=> i.Employee).Include(i=> i.PaidSalaryDuration).Where(i => i.PaidSalaryDurationId == lastPaidSalaryDurationId).ToList();
                DateTime fromDate = _db.PaidSalaryDuration.Where(i => i.Sl == lastPaidSalaryDurationId).Select(i => i.FromDate).FirstOrDefault();
                DateTime toDate = _db.PaidSalaryDuration.Where(i => i.Sl == lastPaidSalaryDurationId).Select(i => i.ToDate).FirstOrDefault();
                ViewBag.FromDate = fromDate.ToShortDateString();
                ViewBag.ToDate = toDate.ToShortDateString();
            }
            return View(salarySheet);
        }

        public ActionResult PaidSalaryDurationList()
        {
            return View(_db.PaidSalaryDuration.ToList());
        }

        public ActionResult IsPaid(int id)
        {
            try
            {
                int lastPaidSalaryDurationId = id;
                PaidSalaryDuration lastPaidSalary = _db.PaidSalaryDuration.Find(lastPaidSalaryDurationId);
                lastPaidSalary.IsPaid = true;
                lastPaidSalary.PaidDate = DateTime.Now;
                _db.Entry(lastPaidSalary).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("PaidSalaryDurationList", "SalarySheet");
            }
            catch
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
                return RedirectToAction("PaidSalaryDurationList", "SalarySheet");
            }
        }

        #region Generate Salary Sheet
        public ActionResult CalculateSalarySheet(DateTime startDate, DateTime endDate)
        {
            DateTime toDate = Utility.Utility.GetDefaultDate();
            int lastPaidSalaryDurationId = 0;
            if (_db.PaidSalaryDuration.ToList().Count > 0)
            {
                lastPaidSalaryDurationId = _db.PaidSalaryDuration.Max(i => i.Sl);
            }
            if (lastPaidSalaryDurationId > 0)
            {
                toDate = _db.PaidSalaryDuration.Where(i => i.Sl == lastPaidSalaryDurationId).Select(i => i.ToDate).FirstOrDefault();
            }
            int pendingSheet = _db.PaidSalaryDuration.Where(i => i.IsPaid == false).ToList().Count;
            if(startDate.Date > toDate.Date && endDate.Date < DateTime.Now.Date && pendingSheet == 0)
            {
                _att.SyncAttendance();
                List<int> employeeSlList = GetEmployeeSlFromMonthlyAttendance(startDate, endDate);
                double workingDays = GetWorkingDays(startDate);
                int paidSalaryDurationId = InsertPaidSalaryDuration(startDate, endDate, workingDays);

                if (paidSalaryDurationId > 0)
                {
                    foreach (var sl in employeeSlList)
                    {
                        double grossSalary = GetGrossSalary(sl);
                        double basicSalary = GetBasicSalary(sl);
                        double perDaySalary = GetPerDaySalary(grossSalary, workingDays);
                        double daysOfSalary = GetDaysOfSalary(startDate, endDate);
                        double employeeGrossSalary = GetEmployeeGrossSalary(perDaySalary, daysOfSalary);
                        double absentDays = GetAbsentDays(sl, startDate, endDate);
                        double absentPanelty = GetAbsentPanelty(absentDays, perDaySalary);
                        double lateDays = GetLateDays(sl, startDate, endDate);
                        double withoutPayLeave = GetWithoutPayLeave(sl);

                        if (lateDays > 0)
                        {
                            int leavePaneltyDays = 0;
                            int branchId = GetEmployeeBranchId(sl);
                            double lateConsiderationDays = (double)GetLateConsiderationDays(branchId);
                            if (lateConsiderationDays > 0)
                            {
                                leavePaneltyDays = GetLeavePaneltyDays(lateDays, lateConsiderationDays);
                            }
                            double earnLeave = GetEarnLeave(sl);
                            if (CalculateLeavePanelty(sl, leavePaneltyDays, earnLeave, withoutPayLeave))
                            {
                                EmployeeLeaveCountHistory empLeaveCount = new EmployeeLeaveCountHistory();
                                empLeaveCount.EmployeeId = sl;
                                empLeaveCount.PaidSalaryDurationId = paidSalaryDurationId;
                                if (earnLeave > leavePaneltyDays)
                                {
                                    empLeaveCount.EarnLeaveDays = leavePaneltyDays;
                                }
                                else
                                {
                                    empLeaveCount.EarnLeaveDays = earnLeave;
                                }
                                empLeaveCount.WithoutPayLeaveDays = withoutPayLeave;
                                _db.EmployeeLeaveCountHistory.Add(empLeaveCount);
                                _db.SaveChanges();
                                //leave counts updated
                            }
                            else
                            {
                                //leave counts calculation failed
                            }
                        }

                        double latePanelty = 0;
                        withoutPayLeave = GetWithoutPayLeave(sl);
                        double leavePanelty = GetLeavePanelty(sl, perDaySalary, withoutPayLeave);
                        double unofficialDays = GetUnofficialDays(sl, startDate, endDate);
                        double unofficialPanelty = GetUnofficialPanelty(perDaySalary, unofficialDays);

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

                        double othersPanelty = GetOthersPanelty(sl, startDate, endDate);
                        double othersBonus = GetOthersBonus(sl, startDate, endDate);
                        double festivalBonus = GetFestivalBonus(sl, startDate, endDate);
                        double adjustmentAmount = GetAdjustmentAmount(sl, startDate, endDate);
                        double loanAmount = GetLoanAmount(sl, paidSalaryDurationId);

                        MonthlySalarySheet monthlySalarySheet = new MonthlySalarySheet();
                        monthlySalarySheet.EmployeeId = sl;
                        monthlySalarySheet.PaidSalaryDurationId = paidSalaryDurationId;
                        monthlySalarySheet.GrossSalary = grossSalary;
                        monthlySalarySheet.BasicSalary = basicSalary;
                        monthlySalarySheet.AbsentDay = absentDays;
                        monthlySalarySheet.AbsentPanelty = absentPanelty;
                        monthlySalarySheet.LateDay = lateDays;
                        monthlySalarySheet.LatePenalty = latePanelty;
                        monthlySalarySheet.UnofficialDay = unofficialDays;
                        monthlySalarySheet.UnofficialPenalty = unofficialPanelty;
                        monthlySalarySheet.LeavePenalty = leavePanelty;
                        monthlySalarySheet.OthersPenalty = othersPanelty;
                        monthlySalarySheet.OthersBonus = othersBonus;
                        monthlySalarySheet.FestivalBonus = festivalBonus;
                        monthlySalarySheet.AdjustmentAmount = adjustmentAmount;
                        monthlySalarySheet.LoanAmount = loanAmount;
                        monthlySalarySheet.NetPay = CalculateNetPay(employeeGrossSalary, absentPanelty, latePanelty, unofficialPanelty, leavePanelty, othersPanelty, othersBonus, festivalBonus, adjustmentAmount, loanAmount);
                        _db.MonthlySalarySheet.Add(monthlySalarySheet);
                        _db.SaveChanges();
                    }
                    UpdateMonthlyAttendanceStatus(startDate, endDate);
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                }
                else
                {
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DateRangeExceed);
                }
            }
            else
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DateRangeExceed);
            }
            return RedirectToAction("Index");
        }

        public List<int> GetEmployeeSlFromMonthlyAttendance(DateTime startDate, DateTime endDate)
        {
            return _db.MonthlyAttendance.Where(i => i.IsCalculated == false && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList().Select(m => m.EmployeeId).Distinct().ToList();
        }

        public List<int> GetEmployeeSlFromMonthlyAttendanceForReverse(DateTime startDate, DateTime endDate)
        {
            return _db.MonthlyAttendance.Where(i => i.IsCalculated == true && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList().Select(m => m.EmployeeId).Distinct().ToList();
        }

        public double GetWorkingDays(DateTime startDate)
        {
            return System.DateTime.DaysInMonth(startDate.Year, startDate.Month);
        }

        public double GetDaysOfSalary(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).TotalDays + 1;
        }

        public int InsertPaidSalaryDuration(DateTime startDate, DateTime endDate, double workingDays)
        {
            int sl = 0;
            try
            {
                PaidSalaryDuration paidSalaryDuration = new PaidSalaryDuration();
                paidSalaryDuration.FromDate = startDate;
                paidSalaryDuration.ToDate = endDate;
                paidSalaryDuration.WorkingDay = workingDays;
                paidSalaryDuration.GenerateDate = DateTime.Now;
                paidSalaryDuration.IsPaid = false;
                paidSalaryDuration.PaidDate = null;
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

        public double GetPerDaySalary(double grossSalary, double workingDays)
        {
            double perDaySalary = 0;
            if(grossSalary > 0 && workingDays > 0)
            {
                perDaySalary = grossSalary / workingDays;
            }
            return perDaySalary;
        }

        public double GetEmployeeGrossSalary(double perDaySalary, double daysOfSalary)
        {
            double employeeGrossSalary = 0;
            if (perDaySalary > 0 && daysOfSalary > 0)
            {
                employeeGrossSalary = daysOfSalary * perDaySalary;
            }
            return employeeGrossSalary;
        }

        public double GetAbsentDays(int sl, DateTime startDate, DateTime endDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date && i.Status == "A" && i.IsCalculated == false).ToList().Count;
        }

        public double GetAbsentPanelty(double absentDays, double perDaySalary)
        {
            double absentPanelty = 0;
            if(absentDays > 0 && perDaySalary > 0)
            {
                absentPanelty = absentDays * perDaySalary;
            }
            return absentPanelty;
        }

        public double GetLateDays(int sl, DateTime startDate, DateTime endDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date && i.Status == "L" && i.IsCalculated == false).ToList().Count;
        }

        public int GetEmployeeBranchId(int sl)
        {
            return _db.Employee.Where(i => i.Sl == sl).Select(i => i.BranchId).FirstOrDefault();
        }

        public double? GetLateConsiderationDays(int branchId)
        {
            return _db.Branches.Where(i => i.Sl == branchId).Select(i => i.LateConsiderationDay).FirstOrDefault();
        }

        public int GetLeavePaneltyDays(double lateDays, double lateConsiderationDays)
        {
            int leavePanelty = 0;
            if(lateDays > 0 && lateConsiderationDays > 0)
            {
                leavePanelty = Convert.ToInt32(Math.Floor(lateDays / lateConsiderationDays));
            }
            return leavePanelty;
        }

        public double GetEarnLeave(int sl)
        {
            return _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Earn").Select(i => i.AvailableDay).FirstOrDefault();
        }

        public double GetWithoutPayLeave(int sl)
        {
            return _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Without Pay").Select(i => i.AvailableDay).FirstOrDefault();
        }

        public bool CalculateLeavePanelty(int sl, int leavePaneltyDays, double EarnLeave, double WithoutPayLeave)
        {
            try
            {
                double result = EarnLeave - leavePaneltyDays;
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

        public double GetLeavePanelty(int sl, double perDaySalary, double WithoutPayLeave)
        {
            double leavePanelty = 0;
            if(perDaySalary > 0 && WithoutPayLeave > 0)
            {
                leavePanelty = perDaySalary * WithoutPayLeave;

                int withoutPayLeaveId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Without Pay").Select(i => i.Sl).FirstOrDefault();
                var withoutPayLeave = _db.LeaveCounts.Find(withoutPayLeaveId);
                withoutPayLeave.AvailableDay = 0;
                _db.Entry(withoutPayLeave).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                leavePanelty = 0;
            }
            return leavePanelty;
        }

        public double GetUnofficialDays(int sl, DateTime startDate, DateTime endDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date && i.Status == "U" && i.IsCalculated == false).ToList().Count;
        }

        public DateTime GetJoiningDate(int sl)
        {
            return _db.Employee.Where(i => i.Sl == sl).Select(i => i.DateOfJoining).FirstOrDefault();
        }

        public bool CheckJoiningDateContains(DateTime startDate, DateTime endDate, DateTime joiningDate)
        {
            if(joiningDate.Date >= startDate.Date && joiningDate.Date <= endDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CalculateBeforeJoiningDays(DateTime startDate, DateTime joiningDate)
        {
            double beforeJoiningDays = 0;
            if(joiningDate.Date > startDate.Date)
            {
                beforeJoiningDays = joiningDate.Day - startDate.Day;
            }
            else
            {
                beforeJoiningDays = 0;
            }
            return beforeJoiningDays;
        }

        public double GetBeforeJoiningDays(int sl, DateTime startDate, DateTime endDate)
        {
            double beforeJoiningDays = 0;
            DateTime joiningDate = GetJoiningDate(sl);
            if(CheckJoiningDateContains(startDate, endDate, joiningDate))
            {
                beforeJoiningDays = CalculateBeforeJoiningDays(startDate, joiningDate);
            }
            else
            {
                beforeJoiningDays = 0;
            }
            return beforeJoiningDays;
        }

        public DateTime GetResignDate(int sl)
        {
            return _db.Resignation.Where(i => i.EmployeeId == sl && i.Status == "Approved").Select(i => i.ResignDate).FirstOrDefault();
        }

        public bool CheckResignDateContains(DateTime startDate, DateTime endDate, DateTime resignDate)
        {
            if (resignDate.Date >= startDate.Date && resignDate.Date <= endDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CalculateAfterResignDays(DateTime endDate, DateTime resignDate)
        {
            double afterResignDays = 0;
            if (resignDate.Date < endDate.Date)
            {
                afterResignDays = endDate.Day - resignDate.Day;
            }
            else
            {
                afterResignDays = 0;
            }
            return afterResignDays;
        }

        public double GetAfterResignDays(int sl, DateTime startDate, DateTime endDate)
        {
            double afterResignDays = 0;
            DateTime resignDate = GetResignDate(sl);
            if (CheckResignDateContains(startDate, endDate, resignDate))
            {
                afterResignDays = CalculateAfterResignDays(endDate, resignDate);
            }
            else
            {
                afterResignDays = 0;
            }
            return afterResignDays;
        }

        public double GetUnofficialPanelty(double perDaySalary, double unofficialDays)
        {
            double unofficialPanelty = 0;
            if(perDaySalary > 0 && unofficialDays > 0)
            {
                unofficialPanelty = perDaySalary * unofficialDays;
            }
            else
            {
                unofficialPanelty = 0;
            }
            return unofficialPanelty;
        }

        public double GetOthersPanelty(int sl, DateTime startDate, DateTime endDate)
        {
            if(_db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Penalty" && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList().Count > 0)
            {
                return _db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Penalty" && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).Sum(i => i.Amount);
            }
            else
            {
                return 0;
            }            
        }

        public double GetOthersBonus(int sl, DateTime startDate, DateTime endDate)
        {
            if(_db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Bonus" && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList().Count > 0)
            {
                return _db.BonusAndPenalty.Where(i => i.EmployeeId == sl && i.Type == "Bonus" && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).Sum(i => i.Amount);
            }
            else
            {
                return 0;
            }
        }

        public double GetFestivalBonus(int sl, DateTime startDate, DateTime endDate)
        {
            double festivalBonus = 0;
            List<FestivalBonus> festivalBonusList =  _db.FestivalBonus.Where(i => DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList();
            if(festivalBonusList.Count > 0)
            {
                foreach(var item in festivalBonusList)
                {
                    if(item.BasedOn == "Gross")
                    {
                        double grossSalary = GetGrossSalary(sl);
                        festivalBonus = festivalBonus + grossSalary * item.BonusPersentage / 100;
                    }
                    else if(item.BasedOn == "Basic")
                    {
                        double basicSalary = GetBasicSalary(sl);
                        festivalBonus = festivalBonus + basicSalary * item.BonusPersentage / 100;
                    }
                    else
                    {
                        festivalBonus = 0;
                    }
                }
            }
            return festivalBonus;
        }

        public double GetAdjustmentAmount(int sl, DateTime startDate, DateTime endDate)
        {
            if (_db.SalaryAdjustment.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList().Count > 0)
            {
                return _db.SalaryAdjustment.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).Sum(i => i.Amount);
            }
            else
            {
                return 0;
            }
        }

        public double GetLoanAmount(int sl, int paidSalaryDurationId)
        {
            List<LoanCalculation> loanList = _db.LoanCalculation.Where(i => i.EmployeeId == sl).ToList();
            double loanAmount = 0;
            if (loanList.Count > 0)
            {
                foreach(var item in loanList)
                {
                    if(item.LoanDuration > 0)
                    {
                        double amount = item.LoanAmount / item.LoanDuration;

                        LoanCalculation calculation = _db.LoanCalculation.Find(item.Sl);
                        calculation.LoanAmount -= amount;
                        calculation.LoanDuration -= 1;
                        _db.Entry(calculation).State = EntityState.Modified;
                        _db.SaveChanges();

                        LoanCalculationHistory history = new LoanCalculationHistory();
                        history.EmployeeId = sl;
                        history.LoanCalculationId = calculation.Sl;
                        history.PaidSalaryDurationId = paidSalaryDurationId;
                        history.LoanAmount = amount;
                        _db.LoanCalculationHistory.Add(history);
                        _db.SaveChanges();

                        loanAmount += amount;
                    }
                }
                return loanAmount;
            }
            else
            {
                return loanAmount;
            }
        }

        public double CalculateNetPay(double employeeGrossSalary, double absentPanelty, double latePanelty,double unofficialPanelty,double leavePanelty,double othersPanelty,double othersBonus,double festivalBonus, double adjustmentAmount, double loanAmount)
        {
            return employeeGrossSalary - absentPanelty - latePanelty - unofficialPanelty - leavePanelty - othersPanelty + othersBonus + festivalBonus + adjustmentAmount - loanAmount;
        }

        public List<MonthlyAttendance> GetMonthlyAttendance(DateTime startDate, DateTime endDate)
        {
            return _db.MonthlyAttendance.Where(i => DbFunctions.TruncateTime(i.Date) >= startDate.Date && DbFunctions.TruncateTime(i.Date) <= endDate.Date).ToList();
        }

        public bool UpdateMonthlyAttendanceStatus(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<MonthlyAttendance> employeeAttendance = GetMonthlyAttendance(startDate, endDate);
                if (employeeAttendance.Count > 0)
                {
                    employeeAttendance.ForEach(x => x.IsCalculated = true);
                }
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Reverse Salary Sheet
        public ActionResult ReverseSalarySheet(int id)
        {
            try
            {
                int lastPaidSalaryDurationId = id;
                var lastPaidSalary = _db.PaidSalaryDuration.Find(lastPaidSalaryDurationId);
                DateTime fromDate = _db.PaidSalaryDuration.Where(i => i.Sl == lastPaidSalaryDurationId).Select(i => i.FromDate).FirstOrDefault();
                DateTime toDate = _db.PaidSalaryDuration.Where(i => i.Sl == lastPaidSalaryDurationId).Select(i => i.ToDate).FirstOrDefault();
                List<int> employeeSlList = GetEmployeeSlFromMonthlyAttendanceForReverse(fromDate, toDate);
                UpdateReverseMonthlyAttendanceStatus(fromDate, toDate);
                RemoveMonthlySalarySheetData(lastPaidSalaryDurationId);
                foreach (var sl in employeeSlList)
                {
                    ReverseLeaveCounts(sl, lastPaidSalaryDurationId);
                    ReverseLoanCalculation(sl, lastPaidSalaryDurationId);
                }
                RemovePaidSalaryDuration(lastPaidSalaryDurationId);
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("PaidSalaryDurationList", "SalarySheet");
            }
            catch
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
                return RedirectToAction("PaidSalaryDurationList", "SalarySheet");
            }
        }

        public bool UpdateReverseMonthlyAttendanceStatus(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<MonthlyAttendance> employeeAttendance = GetMonthlyAttendance(startDate, endDate);
                if (employeeAttendance.Count > 0)
                {
                    employeeAttendance.ForEach(x => x.IsCalculated = false);
                }
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMonthlySalarySheetData(int lastPaidSalaryDurationId)
        {
            try
            {
                _db.MonthlySalarySheet.RemoveRange(_db.MonthlySalarySheet.Where(u => u.PaidSalaryDurationId == lastPaidSalaryDurationId));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemovePaidSalaryDuration(int lastPaidSalaryDurationId)
        {
            try
            {
                PaidSalaryDuration paidSalaryDuration = _db.PaidSalaryDuration.Find(lastPaidSalaryDurationId);
                _db.PaidSalaryDuration.Remove(paidSalaryDuration);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReverseLeaveCounts(int sl, int lastPaidSalaryDurationId)
        {
            try
            {
                int leaveCountHistoryId = _db.EmployeeLeaveCountHistory.Where(i => i.EmployeeId == sl && i.PaidSalaryDurationId == lastPaidSalaryDurationId).Select(i=> i.Sl).FirstOrDefault();
                EmployeeLeaveCountHistory history = _db.EmployeeLeaveCountHistory.Find(leaveCountHistoryId);
                double earnLeave = history.EarnLeaveDays;
                double withoutPayLeave = history.WithoutPayLeaveDays;
                UpdateEmployeeLeaveCounts(sl, earnLeave, withoutPayLeave);
                _db.EmployeeLeaveCountHistory.Remove(history);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReverseLoanCalculation(int sl, int lastPaidSalaryDurationId)
        {
            try
            {
                List<LoanCalculationHistory> loanCalculationHistoryList = _db.LoanCalculationHistory.Where(i => i.EmployeeId == sl && i.PaidSalaryDurationId == lastPaidSalaryDurationId).ToList();
                if(loanCalculationHistoryList.Count > 0)
                {
                    foreach (var item in loanCalculationHistoryList)
                    {
                        LoanCalculation calculation = _db.LoanCalculation.Find(item.LoanCalculationId);
                        calculation.LoanAmount += item.LoanAmount;
                        calculation.LoanDuration += 1;
                        _db.Entry(calculation).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    _db.LoanCalculationHistory.RemoveRange(_db.LoanCalculationHistory.Where(u => u.EmployeeId == sl && u.PaidSalaryDurationId == lastPaidSalaryDurationId));
                    _db.SaveChanges();
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEmployeeLeaveCounts(int sl, double earnLeave, double withoutPayLeave)
        {
            try
            {
                int earnLeaveCountId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Earn").Select(i => i.Sl).FirstOrDefault();
                var earnLeaveCount = _db.LeaveCounts.Find(earnLeaveCountId);
                earnLeaveCount.AvailableDay = earnLeaveCount.AvailableDay + earnLeave;
                _db.Entry(earnLeaveCount).State = EntityState.Modified;
                _db.SaveChanges();

                int withoutLeaveCountId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Without Pay").Select(i => i.Sl).FirstOrDefault();
                var withoutLeaveCount = _db.LeaveCounts.Find(withoutLeaveCountId);
                withoutLeaveCount.AvailableDay = withoutLeaveCount.AvailableDay + withoutPayLeave;
                _db.Entry(withoutLeaveCount).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
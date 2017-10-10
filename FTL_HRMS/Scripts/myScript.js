$("#EmployeeCode").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/Employees/GetExistingEmployee",
            type: "POST",
            dataType: "json",
            data: { CodeOrName: $("#EmployeeCode").val() },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.Code + " : " + item.Name, code: item.Code, value: item.Sl };
                }));
            }
        });
    },
    select: function (event, ui) {
        $("#EmployeeCode").val(ui.item.code);
        $("#EmployeeId").val(ui.item.value);
        return false;
    }
});

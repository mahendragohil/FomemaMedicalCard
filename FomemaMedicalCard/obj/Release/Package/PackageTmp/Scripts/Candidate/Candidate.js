$(document).ready(function () {

    $('#welcome-div').removeClass("custom-hide");

    window.setInterval(function () {
        $('#welcome-div').addClass("custom-hide");
    }, 3000);

    $.ajax({
        type: "POST",
        url: "/Candidate/GetAllCandidate",       
        cache: false,
        success: function (data) {
            debugger;
            bindCandidateGrid(data);
        }
    });

    function bindCandidateGrid(data)
    {
        $("#candidateGrid").jsGrid({
            height: 'auto',
            shrinkToFit: false,
            forceFit: true,
            editing: true,
            autoload: true,
            paging: true,
            deleteConfirm: function (item) {
                return "The candidate \"" + item.Name + "\" will be removed. Are you sure?";
            },
            rowClick: function (args) {
                showDetailsDialog("Edit", args.item);
            },
            data: JSON.parse(data),            
            fields: [
                { name: "Name", title: "Name", type: "text", width:400 },
                { name: "Age", title: "Age", type: "number", width: 80 },
                { name: "NewPassportNo", title: "New Passport No.", type: "text", width: 160 },
                { name: "OldPassportNo", title: "Old Passport No.", type: "text", width: 160 },
                { name: "CountryName", title: "Country Name", type: "text", width: 160 },
                { 
                    name: "Covid19TestDate1",
                    title: "Covid19 TestDate-1",
                    type: "date",
                    width: 170,
                    editable: true                    
                },
                { name: "Covid19TestDate2", title: "Covid19 TestDate-2", type: "date", width: 170 },
                { name: "Covid19TestDate3", title: "Covid19 TestDate-3", type: "date", width: 170 },
                { name: "Covid19TestDate4", title: "Covid19 TestDate-4", type: "date", width: 170 },
                { name: "ClinicName", title: "Clinic Name", type: "text", width: 200 },
                { name: "ClinicLocation", title: "Clinic Location", type: "text", width: 500 },
                { name: "VaccineDose1Date", title: "Vaccine Dose Date-1", type: "date", width: 190 },
                { name: "VaccineDose2Date", title: "Vaccine Dose Date-2", type: "date", width: 190 },
                //{ name: "Country Name", type: "text", width: 50 },
                //{ name: "Covid19 Test Date 1", type: "date", width: 50 },
                //{ name: "Covid19 Test Date 1", type: "date", width: 50 },

                //{ name: "Address", type: "text", width: 200 },
                //{ name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name" },
                //{ name: "Married", type: "checkbox", title: "Is Married", sorting: false },
                {
                    type: "control",
                    modeSwitchButton: false,
                    editButton: false,
                    width: 100,
                    headerTemplate: function () {
                        return $("<button>").attr("type", "button").text("Add")
                            .on("click", function () {
                                showDetailsDialog("Add", {});
                            });
                    }
                }
            ]        
           
        });
    }
        

    $("#detailsDialog").dialog({
        autoOpen: false,
        height:400,
        width: 450,
        close: function () {
            $("#detailsForm").validate().resetForm();
            $("#detailsForm").find(".error").removeClass("error");
        }
    });

    $("#detailsForm").validate({
        rules: {
            name: "required",
            age: { required: true, range: [18, 150] },
            address: { required: true, minlength: 10 },
            country: "required"
        },
        messages: {
            name: "Please enter name",
            age: "Please enter valid age",
            address: "Please enter address (more than 10 chars)",
            country: "Please select country"
        },
        submitHandler: function () {
            formSubmitHandler();
        }
    });

    var formSubmitHandler = $.noop;

    function showDetailsDialog(dialogType, candidate) {
        $("#name").val(candidate.Name);
        $("#age").val(candidate.Age);
        $("#newpassportno").val(candidate.NewPassportNo);
        $("#oldpassportno").val(candidate.OldPassportNo);
        $("#countryname").val(candidate.CountryName);
        $("#covid19testdate1").val(candidate.Covid19TestDate1);
        $("#covid19testdate2").val(candidate.Covid19TestDate2);
        $("#covid19testdate3").val(candidate.Covid19TestDate3);
        $("#covid19testdate4").val(candidate.Covid19TestDate4);
        $("#clinicname").val(candidate.ClinicName);
        $("#cliniclocation").val(candidate.ClinicLocation);
        $("#vaccinedose1date").val(candidate.VaccineDose1Date);
        $("#vaccinedose2date").val(candidate.VaccineDose2Date);
       
        formSubmitHandler = function () {
            saveCandidate(candidate, dialogType === "Add");
        };

        $("#detailsDialog").dialog("option", "title", dialogType + " Candidate")
            .dialog("open");
    };

    function saveCandidate(client, isNew) {
        $.extend(client, {
            Name: $("#name").val(),
            Age: parseInt($("#age").val(), 10),
            Address: $("#address").val(),
            Country: parseInt($("#country").val(), 10),
            Married: $("#married").is(":checked")
        });

        $("#candidateGrid").jsGrid(isNew ? "insertItem" : "updateItem", client);

        $("#detailsDialog").dialog("close");
    };
});
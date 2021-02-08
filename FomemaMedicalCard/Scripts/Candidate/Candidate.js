$(document).ready(function () {
    $('#welcome-div').removeClass("custom-hide");

    window.setInterval(function () {
        $('#welcome-div').addClass("custom-hide");
    }, 3000);

    //$.ajax({
    //    type: "POST",
    //    url: "/Candidate/GetAllCandidate",
    //    cache: false,
    //    success: function (data) {
    //        bindCandidateGrid(data);
    //    }
    //});
    $('.datepicker').each(function () {
        $("#" + $(this).attr("Id")).datepicker({
            dateFormat: 'dd-mm-yy',
            autoclose: true,
            maxDate: 0 // so 0 represents today. disable future date.
        });
    });
    function bindCandidateGrid() {



        $("#candidateGrid").jsGrid({
            width: '100%',
            height: 'auto',
            shrinkToFit: true,
            forceFit: true,
            editing: true,
            filtering: (UserType == 1),
            sorting: true,
            sortLoading: true,

            autoload: true,
            loadIndication: true,
            loadIndicationDelay: 500,
            loadMessage: "Fetching Data...",
            loadShading: true,

            updateOnResize: true,

            paging: true,
            pageLoading: true,

            pageIndex: 1,
            pageSize: 4,
            pageButtonCount: 5,
            //pagerFormat: "Pages: {first} {prev} {pages} {next} {last} &nbsp;&nbsp; {pageIndex} of {pageCount}",
            //pagePrevText: "Prev",
            //pageNextText: "Next",
            //pageFirstText: "First",
            //pageLastText: "Last",
            //pageNavigatorNextText: "...",
            //pageNavigatorPrevText: "...",

            noDataContent: "No Candidate In The System",
            onItemEditing: function (args) {
                showDetailsDialog("Edit", args.item);
                args.cancel = true;
            },
            deleteConfirm: function (item) {
                return "The candidate \"" + item.Name + "\" will be removed. Are you sure?";
            },
            rowClick: function (args) {
                showDetailsToUser(args.item);
            },
            //data: JSON.parse(data),
            controller: {
                //loadData: function (filter) {
                //    return $.ajax({
                //        type: "GET",
                //        data: filter,
                //        url: "/Candidate/GetAllCandidate",
                //        dataType: "json"
                //    });
                //},
                loadData: function (filter) {

                    criteria = filter;
                    criteria.UserType = UserType;
                    criteria.SearchPassportNo = $("#searchPassportNo").val();
                    var result = $.Deferred();
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: "/Candidate/GetAllCandidate",
                        data: criteria,
                        dataType: "json"
                    }).done(function (data) {
                        var response = JSON.parse(data);
                        var res = response.Item1;

                        if (res.length > 0) {
                            result.resolve(
                                {
                                    data: res,
                                    itemsCount: response.Item2
                                });
                        }
                        else {
                            result.resolve(
                                {
                                    data: res,
                                    itemsCount: 0
                                });
                        }
                    });
                    return result.promise();
                },
                insertItem: function (item) {
                    var insertData = getDataFromJsGrid();
                    // actuall data send to server
                    return $.ajax({
                        type: "POST",
                        url: "/Candidate/SaveCandidateDetails/",
                        data: insertData,
                        //dataType: "json"
                        processData: false,
                        contentType: false
                    });
                },
                updateItem: function (item) {
                    var updateData = getDataFromJsGrid();
                    // actuall data send to server
                    return $.ajax({
                        type: "POST",
                        url: "/Candidate/SaveCandidateDetails/",
                        data: updateData,
                        //dataType: "json",
                        processData: false,
                        contentType: false
                    });
                },
                deleteItem: function (item) {
                    //var deleteData = getDataFromJsGrid();
                    // actuall data send to server
                    return $.ajax({
                        type: "POST",
                        url: "/Candidate/DeleteCandidate/",
                        data: item,
                        dataType: "json",
                    });
                },
            },
            fields: [
                { name: "Name", title: "Name", type: "text", width: 300 },
                { name: "DateofBirth", title: "Date of Birth", type: "date", width: 120 },
                { name: "Age", title: "Age", type: "number", width: 60 },
                { name: "NewPassportNo", title: "New Passport No.", type: "text", width: 160 },
                { name: "OldPassportNo", title: "Old Passport No.", type: "text", width: 160 },
                { name: "CountryName", title: "Country Name", type: "text", width: 160 },
                { name: "FomemaTestYear", title: "Fomema Test Year", type: "number", width: 100 },
                {
                    name: "FomemaTestResult", title: "Fomema Test Result", type: "text", width: 140, cellRenderer: function (value, item) {
                        switch (value.toLowerCase()) {
                            case 'suitable':
                                return '<td class="jsgrid-cell bg-success text-center text-light">' + value + '</td>';
                            case 'unsuitable':
                                return '<td class="jsgrid-cell bg-danger text-center text-light">' + value + '</td>';
                            default:
                                return '<td class="jsgrid-cell text-center" style="width: 140px;">' + value + '</td>';
                        }
                    }
                },

                //Need to show in another grid
                { name: "Covid19TestDate1", title: "Covid19 TestDate-1", type: "date", width: 170, visible: false },
                { name: "Covid19TestDate2", title: "Covid19 TestDate-2", type: "date", width: 170, visible: false },
                { name: "Covid19TestDate3", title: "Covid19 TestDate-3", type: "date", width: 170, visible: false },
                { name: "Covid19TestDate4", title: "Covid19 TestDate-4", type: "date", width: 170, visible: false },
                { name: "ClinicName", title: "Clinic Name", type: "text", width: 200, visible: false },
                { name: "ClinicLocation", title: "Clinic Location", type: "text", width: 500, visible: false },
                { name: "VaccineDose1Date", title: "Vaccine Dose Date-1", type: "date", width: 190, visible: false },
                { name: "VaccineDose2Date", title: "Vaccine Dose Date-2", type: "date", width: 190, visible: false },

                //{ name: "Country Name", type: "text", width: 50 },
                //{ name: "Covid19 Test Date 1", type: "date", width: 50 },
                //{ name: "Covid19 Test Date 1", type: "date", width: 50 },

                //{ name: "Address", type: "text", width: 200 },
                //{ name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name" },
                //{ name: "Married", type: "checkbox", title: "Is Married", sorting: false },
                {
                    type: "control",
                    modeSwitchButton: false,
                    editButton: true,
                    deleteButton: (UserType == 1),
                    width: 100,
                    headerTemplate: function () {
                        return $("<button>").attr("type", "button").addClass("jsgrid-button jsgrid-insert-button")
                            .on("click", function () {
                                showDetailsDialog("Add", {});
                            });
                    }
                }
            ]
        });
    }

    bindCandidateGrid();

    $("#detailsDialog").dialog({
        autoOpen: false,
        height: 400,
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
            if ($("#detailsForm").validate()) {
                // on  button click first call coming here.
                formSubmitHandler();
            }
        }
    });

    var formSubmitHandler = $.noop;

    function showDetailsToUser(candidate) {
        if (candidate.PictureURL && typeof (candidate.PictureURL) !== "undefined") {
            $("#DTPictureURL").attr("src", candidate.PictureURL);
        }
        else {
            $("#DTPictureURL").attr("src", "/Content/Images/default-avatar-profile-icon.png");
        }
        
        $("#DTCovid19TestDate1").text((candidate.Covid19TestDate1 == null) ? "" : candidate.Covid19TestDate1);
        $("#DTCovid19TestDate2").text((candidate.Covid19TestDate2 == null) ? "" : candidate.Covid19TestDate2);
        $("#DTCovid19TestDate3").text((candidate.Covid19TestDate3 == null) ? "" : candidate.Covid19TestDate3);
        $("#DTCovid19TestDate4").text((candidate.Covid19TestDate4 == null) ? "" : candidate.Covid19TestDate4);
        $("#DTClinicName").text(candidate.ClinicName);
        $("#DTClinicLocation").text(candidate.ClinicLocation);
        $("#DTVaccineDose1Date").text((candidate.VaccineDose1Date == null) ? "" : candidate.VaccineDose1Date);
        $("#DTVaccineDose2Date").text((candidate.VaccineDose2Date == null) ? "" : candidate.VaccineDose2Date);
    };

    function showDetailsDialog(dialogType, candidate) {
        $("#CandidateId").val(candidate.CandidateId);
        $("#CandidateGuid").val(candidate.CandidateGuid);
        $("#CountryName").val(candidate.CountryName);
        if (candidate.PictureURL && typeof (candidate.PictureURL) != "undefined") {
            document.getElementById('snapShot').innerHTML =
                '<img id="PictureURL" src="' + candidate.PictureURL + '"  class="img-fluid mx-auto d-block" />';
        } else {
            document.getElementById('snapShot').innerHTML = "";
        }
        $("#Name").val(candidate.Name);
        $("#DateofBirth").val(candidate.DateofBirth);
        $("#Age").val(candidate.Age);
        $("#NewPassportNo").val(candidate.NewPassportNo);
        $("#OldPassportNo").val(candidate.OldPassportNo);
        $("#FomemaTestYear").val(candidate.FomemaTestYear);
        $("#FomemaTestResult").val(candidate.FomemaTestResult);
        $("#Covid19TestDate1").val(candidate.Covid19TestDate1);
        $("#Covid19TestDate2").val(candidate.Covid19TestDate2);
        $("#Covid19TestDate3").val(candidate.Covid19TestDate3);
        $("#Covid19TestDate4").val(candidate.Covid19TestDate4);
        $("#ClinicName").val(candidate.ClinicName);
        $("#ClinicLocation").val(candidate.ClinicLocation);
        $("#VaccineDose1Date").val(candidate.VaccineDose1Date);
        $("#VaccineDose2Date").val(candidate.VaccineDose2Date);

        formSubmitHandler = function () {
            // second call for save
            saveCandidate(candidate, dialogType === "Add");
        };

        $("#detailsDialog").dialog("option", "title", dialogType + " Candidate")
            .dialog("open");
    };

    function saveCandidate(candidate, isNew) {
        //$.extend(client, {
        //    Name: $("#name").val(),
        //    DateofBirth: $("#dateofbirth").val(),
        //    Age: parseInt($("#age").val(), 10),
        //    Address: $("#address").val(),
        //    Country: parseInt($("#country").val(), 10),
        //    Married: $("#married").is(":checked")
        //});

        $("#candidateGrid").jsGrid(isNew ? "insertItem" : "updateItem", candidate);

        $("#detailsDialog").dialog("close");
    };

    $('#chkIsFilter').change(function () {
        $("#candidateGrid").jsGrid("option", "filtering", $(this).is(":checked"));
    });

    $("#btnSearchFilter").click(function () {
        $("#candidateGrid").jsGrid("search");
    });
    window.setInterval(function () {
        $('#divCandidateDetails,#headerCandidateDetails').show();
        $('#candidateLoading').hide();
    }, 4000);
});
$("#takePicture").click(function () {
    takePicture();
});
function takePicture() {
    Webcam.snap(function (data_uri) {
        document.getElementById('snapShot').innerHTML =
            '<img id="PictureURL" src="' + data_uri + '"  class="img-fluid mx-auto d-block" />';
        Webcam.reset(); // Reset (Shutdown)
        $("#captureImagemodel").dialog("close");
    });
}
function startCamera() {
    $("#captureImagemodel").show();
    Webcam.attach('#webcamLivePicture');
    $("#captureImagemodel").dialog({
        title: "Capture cadidate picture",
        height: 400,
        width: 450,
        close: function () {
            Webcam.reset(); // Reset (Shutdown)
        }
    });
}
function getDataFromJsGrid() {
    var formData = new FormData($('#detailsForm')[0]);
    formData.append('PictureFromComputer', $('input[type=file]')[0].files[0]);
    formData.append('PictureURL', $('#PictureURL').attr("src"));
    candidate = formData;
    return formData;
}
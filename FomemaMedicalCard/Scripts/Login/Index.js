$(document).ready(function () {

    $("#btn-login").click(function () {

        var useId = $("#text-userId").val();
        var password = $("#text-password").val();

        var data =
        {
            userId: useId,
            password: password
        }

        $.ajax({
            type: "POST",
            url: "/Login/Login",
            data: data,
            cache: false,
            success: function (result) {

                if (result) {
                    window.location.href = "/Candidate/CandidateDetails"
                }
                else {
                    $('#login-failed-div').removeClass("custom-hide");
                }
            },
            error: function (xhr, status, error) {

            }
        });

    });

    $("#btn-submit").click(function () {

        var clinicname = $("#text-clinicname").val();
        var doctorsname = $("#text-doctorsname").val();
        var doctorsicnumber = $("#text-doctorsicnumber").val();
        var password = $("#text-clinicpassword").val();
        var confirmPassword = $("#text-clinicconfirmpassword").val();

        if (doctorsicnumber == "") {
            alert('Please enter doctor IC number..!!');
            return;
        }
        if (password == "") {
            alert('Please enter password..!!');
            return;
        }
        if (password != confirmPassword) {
            alert('Password and Confirm Password was not matched..!!');
            return;
        }

        var data =
        {
            clinicname: clinicname,
            doctorsname: doctorsname,
            doctorsicnumber: doctorsicnumber,
            password: password
        }
        $.ajax({
            type: "POST",
            url: "/Login/CheckICNumberAvailibility",
            data: data,
            cache: false,
            success: function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "/Login/ClinicRegistrationSubmit",
                        data: data,
                        cache: false,
                        success: function (result) {
                            if (result) {
                                window.location.href = "/Candidate/CandidateDetails"
                            }
                            else {
                                $('#submit-failed-div').removeClass("custom-hide");
                            }
                        },
                        error: function (xhr, status, error) {

                        }
                    });
                }
                else {
                    alert('Given doctor IC number already exist..!!');
                }
            },
            error: function (xhr, status, error) {

            }
        });

    });

    window.setInterval(function () {
        $('#login-failed-div').addClass("custom-hide");
        $('#submit-failed-div').addClass("custom-hide");
    }, 3000);


});






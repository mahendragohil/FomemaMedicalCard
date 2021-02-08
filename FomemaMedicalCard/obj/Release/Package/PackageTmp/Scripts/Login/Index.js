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
                debugger;
                if (result) {
                    window.location.href = "/Candidate/CandidateDetails"
                }
                else {
                    $('#login-failed-div').removeClass("custom-hide");
                }
            },
            error: function (xhr, status, error) {
                debugger;
            }
        });

    });

    window.setInterval(function () {
        $('#login-failed-div').addClass("custom-hide");
    }, 3000);




});






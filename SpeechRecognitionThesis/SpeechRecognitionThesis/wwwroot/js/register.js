$(document).ready(function() {
    $("#register-form").submit(function(event){
        var serializeData = $(this).serialize();

        $.ajax({
            type        : 'POST',
            url         : '/register',
            data        : serializeData,
            dataType    : 'json',
            encode      : true,
            statusCode: {
                200: 
                    function (data1) {
                        alert('Twoje konto zostało utworzone! Zostaniesz przeniesiony do strony głównej' + data1.responseText);
                        window.location.href = "/";
                    },
                400: 
                    function (data1) {
                        alert('Wystąpił błąd przy tworzeniu twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz' +data1.responseText);
                    }
            }
        })
        .done(function(data) {
            console.log("The registration was successful!"); 
        });
        event.preventDefault();
    });
});
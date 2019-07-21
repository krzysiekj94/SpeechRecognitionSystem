$(document).ready(function() {
    $("#login-form").submit(function(event){
        var serializeData = $(this).serialize();

        $.ajax({
            type        : 'POST',
            url         : '/login',
            data        : serializeData,
            dataType    : 'json',
            encode      : true,
            statusCode: {
                200: 
                    function (data1) {
                        alert('Zostałeś pomyślnie zalogowany! Zostaniesz przeniesiony do strony głównej' + data1.responseText);
                        window.location.href = "/";
                    },
                400: 
                    function (data1) {
                        alert('Wystąpił błąd przy logowaniu do twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz' +data1.responseText);
                    }
            }
        })
        .done(function(data) {
            console.log("The login was successful!"); 
        });
        event.preventDefault();
    });
});
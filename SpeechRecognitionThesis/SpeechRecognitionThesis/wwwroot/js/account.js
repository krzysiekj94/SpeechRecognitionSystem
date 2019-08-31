$(document).ready(function() {
    
    //#TODO To tests
    $(".change-icon").click(function() {
        $("#dialog").dialog();
    });
    
    $("#delete-account").click(function() {
        var serializeData = $(this).serialize();

        $.ajax({
            type        : 'POST',
            url         : '/account/delete',
            dataType    : 'json',
            encode      : true,
            statusCode: {
                200: 
                    function (data1) {
                        alert('Twoje konto zostało usunięte! Zostaniesz przeniesiony do strony głównej');
                        window.location.href = "/";
                    },
                400: 
                    function (data1) {
                        alert('Wystąpił błąd przy tworzeniu twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz');
                    }
            }
        })
        .done(function(data) {
            console.log("The process delete account was successful!"); 
        });
        
        event.preventDefault();
    });


});
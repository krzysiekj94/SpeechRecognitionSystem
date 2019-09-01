$(document).ready(function() {
    
    $("#change-account-icon").click(function() {
        Swal.fire({
            title: '<strong>Wybierz awatar</strong>',
            type: 'info',
            width: 600,
            html: '<img class="user-icon" src="/images/1.png" />'
            + '<img class="user-icon" src="/images/2.png" />'
            + '<img class="user-icon" src="/images/3.png" />'
            + '<img class="user-icon" src="/images/4.png" />'
            + '<img class="user-icon" src="/images/5.png" />'
            + '<img class="user-icon" src="/images/6.png" />'
            + '<img class="user-icon" src="/images/7.png" />'
            + '<img class="user-icon" src="/images/8.png" />',
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            confirmButtonText:
              '<b>Zapisz wybór!</b>',
            cancelButtonText:
              '<b>Anuluj</b>',
          });
    });
    
    $("#delete-account").click(function() {
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


    $("#change-data-form").submit(function(event){
        var serializeData = $(this).serialize();

        $.ajax({
            type        : 'POST',
            url         : '/account/change',
            data        : serializeData,
            dataType    : 'json',
            encode      : true,
            statusCode: {
                200: 
                    function (data1) {
                        alert('Dane twojego konta zostało zmienione! Zostaniesz przeniesiony do strony głównej');
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
$(document).ready(function() {
    
    function GetUserIconHtmlString()
    {
        var htmlString = '';
        for(i=1; i<=8;i++)
        {
            htmlString += '<img class="user-icon" src="/images/' + i + '.png"/>';
        }
        
        return htmlString;
    }
    
    $("#change-account-icon").click(function() {
        
        var htmlString = GetUserIconHtmlString();

        Swal.fire({
            title: '<strong>Wybierz awatar</strong>',
            type: 'info',
            width: 600,
            html: htmlString,
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            confirmButtonText:
              '<b>Zapisz wybór!</b>',
            cancelButtonText:
              '<b>Anuluj</b>',
          }).then(result => {
            if( result.value ) 
            {
              // handle Confirm button click
              // result.value will contain `true` or the input value
              alert("Działa!");
            } 
            else 
            {
              // handle dismissals
              // result.dismiss can be 'cancel', 'overlay', 'esc' or 'timer'
              alert("Nie działą!");
            }
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

    $('body').on('click','.user-icon',function(){
        var index = $(this).index();
        alert("kliknięty element: " + index );
    });
});
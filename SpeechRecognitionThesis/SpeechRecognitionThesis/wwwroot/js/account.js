$(document).ready(function() {
    
    var indexSelectedIcon = -1;

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
              console.log("Zapis informacji dot. awatara użytkownika!");
              SaveAvatarInfoIntoDB( indexSelectedIcon );
              UpdateUserMainAvatar( indexSelectedIcon );
            } 
            else 
            {
              console.log("Anulowanie zapisu info dot. awatara użytkownika!");
            }

            indexSelectedIcon = -1;
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
        var choosenIconIndex = $(this).index();
        
        if( indexSelectedIcon != -1 )
        {
            $( ".user-icon:eq(" + indexSelectedIcon + ")" ).css( "background-color", "white" );
        }

        indexSelectedIcon = choosenIconIndex;

        $( ".user-icon:eq(" + indexSelectedIcon + ")" ).css( "background-color", "red" );
    });

    function SaveAvatarInfoIntoDB( indexSelectedIcon )
    {
        var userObject = 
        {
            "NickName" : "User",
            "AvatarId" : indexSelectedIcon
        };

        var userUpdateObject = 
        { 
          "User" : userObject,
        };

        var data =  JSON.stringify(userUpdateObject);

        $.ajax({
            type        : 'POST',
            url         : '/account/avatar',
            data        : JSON.stringify(userUpdateObject),
            contentType   : 'application/json; charset=utf-8',
            dataType    : 'json',
            encode      : true,
            statusCode: {
                200: 
                    function (data1) {
                        console.log('Avatar zaktualizowany pomyślnie');
                    },
                400: 
                    function (data1) {
                        console.log('Avatar nie został zaktualizowany pomyślnie');
                    }
            }
        })
        .done(function(data) {
            console.log("Aktualizacja avatara przebiegła pomyślnie"); 
        });
    }

    function UpdateUserMainAvatar( indexSelectedIcon )
    {
        var imageNumber = indexSelectedIcon + 1; 
        $(".main-user-icon").attr("src", "/images/" + imageNumber + ".png");
    }
});
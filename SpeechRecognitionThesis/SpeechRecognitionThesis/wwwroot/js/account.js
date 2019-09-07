$(document).ready(function() {
    
    var firstIndexIcon = parseInt(GetUserIconId());
    var indexSelectedIcon = parseInt(GetUserIconId());

    function GetUserIconId()
    {
        var srcMainIconString = $('.main-user-icon').attr('src');
        var srcNameSplitString =  srcMainIconString.split('.')[0] + '';
        var fileimageSplitString = srcNameSplitString.split('/');
        return fileimageSplitString[fileimageSplitString.length-1];
    }

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
        ChangeAccountIcon();
    });

    function ChangeAccountIcon()
    {
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
              if( firstIndexIcon != indexSelectedIcon )
              {
                  SaveAvatarInfoIntoDB( indexSelectedIcon );
                  UpdateUserMainAvatar( indexSelectedIcon );
                  firstIndexIcon = indexSelectedIcon;
              }
            } 
            else 
            {
              console.log("Anulowanie zapisu info dot. awatara użytkownika!");
              indexSelectedIcon = firstIndexIcon;
            }
        });
    }
    
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
        ChooseImgIcon(choosenIconIndex);
    });

    function ChooseImgIcon(choosenIconIndex)
    {
        chooseIconIndexTemp = choosenIconIndex - 1;

        if( indexSelectedIcon > -1 )
        {
            $( ".user-icon:eq(" + indexSelectedIcon + ")" ).css( "background-color", "white" );
        }

        indexSelectedIcon = chooseIconIndexTemp;

        $( ".user-icon:eq(" + indexSelectedIcon + ")" ).css( "background-color", "red" );
    }

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

$.getScript("/js/speech_engine.js", function(){

    artyom.addCommands([
        {
            indexes: ["zmień nazwę użytkownika", "zmień nick", "zmień nik", 
            "nazwa użytkownika", "nick", "nik", "ustaw nick", "ustaw nazwę",
            "login", "ustaw login", "ustaw nazwę użytkownika", "nazwa"],
            action: function(){
                $("#NickName").focus();
            }
        },
        {
            indexes: ["zmień hasło", "hasło", "ustaw hasło"],
            action: function(){
                $("#Password").focus();
            }
        },
        {
            indexes: ["potwierdź hasło", "potwierdzenie", "potwierdź", "zatwierdź"],
            action: function(){
                $("#ConfirmPassword").focus();
            }
        },
        {
            indexes: ["email", "e-mail", "zmień e-mail", "zmień email"],
            action: function(){
                $("#Email").focus();
            }
        },
        {
            indexes: ["zmień dane", "zapisz dane"],
            action: function(){
                $("#change-account-data").submit();
            }
        },
        {
            indexes: ["usuń konto", "usuń swoje konto"],
            action: function(){
                $("#delete-account").click();
            }
        },
        {
            indexes: ["zmień obrazek"],
            action: function(){
                ChangeAccountIcon();
            }
        },
        {
            indexes: ["zapisz obrazek"],
            action: function(){
                console.log("Zapis informacji dot. awatara użytkownika!");
                Swal.close();
                if( firstIndexIcon != indexSelectedIcon )
                {
                    SaveAvatarInfoIntoDB( indexSelectedIcon );
                    UpdateUserMainAvatar( indexSelectedIcon );
                    firstIndexIcon = indexSelectedIcon;
                }
            }
        },
        {
            indexes: ["anuluj"],
            action: function(){
                console.log("Zamykam dialog wyboru awatara!");
                Swal.close();
                indexSelectedIcon = firstIndexIcon;
            }
        },
        {
            indexes: ["wybierz pierwszy", "wybierz 1", "wybierz jeden"],
            action: function(){
                ChooseImgIcon(1);
            },
        },
        {
            indexes: ["wybierz drugi", "wybierz 2", "wybierz dwa"],
            action: function(){
                ChooseImgIcon(2);
            },
        },
        {
            indexes: ["wybierz trzeci", "wybierz 3", "wybierz trzy"],
            action: function(){
                ChooseImgIcon(3);
            },
        },
        {
            indexes: ["wybierz czwarty", "wybierz 4", "wybierz cztery"],
            action: function(){
                ChooseImgIcon(4);
            },
        },
        {
            indexes: ["wybierz pięć", "wybierz 5", "wybierz pięć"],
            action: function(){
                ChooseImgIcon(5);
            },
        },
        {
            indexes: ["wybierz szósty", "wybierz 6", "wybierz sześć"],
            action: function(){
                ChooseImgIcon(6);
            },
        },
        {
            indexes: ["wybierz siedem", "wybierz 7", "wybierz siedem"],
            action: function(){
                ChooseImgIcon(7);
            },
        },
        {
            indexes: ["wybierz osiem", "wybierz 8", "wybierz osiem"],
            action: function(){
                ChooseImgIcon(8);
            },
        },
    ]);
 });
});
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

    $( ".your-articles-button" ).click(function() {
        window.open("/articles/my","_self");
      });

    function ChangeAccountIcon()
    {
        var htmlString = GetUserIconHtmlString();

        Swal.fire({
            title: '<strong>Wybierz avatar</strong>',
            type: 'info',
            width: 600,
            html: htmlString,
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            confirmButtonText:
              '<b>Zapisz avatar!</b>',
            cancelButtonText:
              '<b>Anuluj</b>',
          }).then(result => {
            if( result.value ) 
            {
              console.log("Zapis informacji dot. avatara użytkownika!");
              if( firstIndexIcon != indexSelectedIcon )
              {
                  SaveAvatarInfoIntoDB( indexSelectedIcon );
                  UpdateUserMainAvatar( indexSelectedIcon );
                  firstIndexIcon = indexSelectedIcon;
              }
            } 
            else 
            {
              console.log("Anulowanie zapisu info dot. avatara użytkownika!");
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
                        Swal.fire({
                            position: 'center',
                            type: 'warning',
                            title: 'Twoje konto zostało usunięte! Zostaniesz przeniesiony do strony głównej!',
                            showConfirmButton: true,
                            timer: 3000
                          }).then(function(){
                            window.location.href = "/";
                          });
                    },
                400: 
                    function (data1) {
                        Swal.fire({
                            position: 'center',
                            type: 'error',
                            title: 'Wystąpił błąd przy tworzeniu twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz!',
                            showConfirmButton: true,
                            timer: 3000
                          }).then(function(){
                            //do nothing
                          });
                    }
            }
        })
        .done(function(data) {
            console.log("The process delete account was done successful!"); 
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
                    function() {
                        Swal.fire({
                            position: 'center',
                            type: 'success',
                            title: 'Dane twojego konta zostało zmienione! Będziesz mógł z nich skorzystać po ponownym zalogowaniu się na swoje konto!',
                            showConfirmButton: true,
                            timer: 5000
                          }).then(function(){
                            //do nothing
                          });
                    },
                400: 
                    function() {
                        Swal.fire({
                            position: 'center',
                            type: 'error',
                            title: 'Wystąpił błąd przy tworzeniu twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz!',
                            showConfirmButton: true,
                            timer: 5000
                          }).then(function(){
                            //do nothing
                          });
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

    LoadLettersAndNumbersCommands();
    LoadSpecialCharactersCommands();
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
            indexes: ["zmień e-mail", "zmień email"],
            action: function(){
                $("#Email").focus();
            }
        },
        {
            indexes: ["wyczyść login", "wyczyść nazwa", "wyczyść nazwę", "wyczyść nazwę użytkownika"],
            action: function(){
                $("#NickName").val("");
                $("#NickName").focus();
            }
        },
        {
            indexes: ["wyczyść hasło"],
            action: function(){
                $("#Password").val("");
                $("#Password").focus();
            }
        },
        {
            indexes: ["wyczyść potwierdzenie", "wyczyść potwierdzenie"],
            action: function(){
                $("#ConfirmPassword").val("");
                $("#ConfirmPassword").focus();
            }
        },
        {
            indexes: ["wyczyść email", "wyczyść e-mail", "wyczyść email'a"],
            action: function(){
                $("#Email").val("");
                $("#Email").focus();
            }
        },
        {
            indexes: ["sprawdź hasło"],
            action: function(){
                $("#Password").attr('type', 'text'); 
                $("#ConfirmPassword").attr('type', 'text'); 
            }
        },
        {
            indexes: ["ukryj hasło"],
            action: function(){
                $("#Password").attr('type', 'password'); 
                $("#ConfirmPassword").attr('type', 'password'); 
            }
        },
        {
            indexes: ["cofnij"],
            action: function(){
                var currentElement = document.activeElement.id;
                var valueElement = "";

                if( currentElement.length > 0 )
                {
                    valueElement =  $( "#" + currentElement ).val();
                    valueElement = valueElement.slice(0, -1);
                    $( "#" + currentElement ).val(valueElement);
                }
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
            indexes: ["zmień avatar"],
            action: function(){
                ChangeAccountIcon();
            }
        },
        {
            indexes: ["zapisz avatar"],
            action: function(){
                console.log("Zapis informacji dot. avatara użytkownika!");
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
                console.log("Zamykam dialog wyboru avatara!");
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
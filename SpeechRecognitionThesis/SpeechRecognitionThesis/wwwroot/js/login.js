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
                    function(){
                        Swal.fire({
                            position: 'center',
                            type: 'success',
                            title: 'Zostałeś pomyślnie zalogowany! Zostaniesz przeniesiony do strony głównej!',
                            showConfirmButton: true,
                            timer: 3000
                          }).then(function(){
                            window.location.href = "/";
                          });
                    },
                400: 
                    function () {
                        Swal.fire({
                            position: 'center',
                            type: 'error',
                            title: 'Wystąpił błąd przy logowaniu do twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz',
                            showConfirmButton: true,
                            timer: 3000
                          }).then(function(){
                            //do nothing
                          });
                    }
            }
        })
        .done(function(data) {
            console.log("The login was successful!"); 
        });
        event.preventDefault();
    });

    $.getScript("/js/speech_engine.js", function(){
               
        var commands = artyom.getAvailableCommands();
        commands.splice(7,1);
        artyom.emptyCommands();

        LoadLettersAndNumbersCommands();
        artyom.addCommands(commands);
        artyom.addCommands([
            {
                indexes: ["ustaw nazwę", "ustaw nick", "login", "ustaw login"],
                action: function(){
                    $("#NickName").focus();
                }
            },
            {
                indexes: ["hasło", "ustaw hasło"],
                action: function(){
                    $("#Password").focus();
                }
            },
            {
                indexes: ["zaloguj", "zaloguj się", "logowanie"],
                action: function(){
                    $(".login-button").submit();
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
                indexes: ["sprawdź hasło"],
                action: function(){
                    $("#Password").attr('type', 'text'); 
                }
            },
            {
                indexes: ["ukryj hasło"],
                action: function(){
                    $("#Password").attr('type', 'password'); 
                }
            },
        ]);
     });
});
$(document).ready(function() {
    $("#register-form").submit(function(event){
        var serializeData = $(this).serialize();

        $.ajax({
            type        : 'POST',
            url         : '/register',
            data        : serializeData,
            dataType    : 'json',
            encode      : true,
            statusCode  : {
                200: 
                    function() {
                        Swal.fire({
                            position: 'center',
                            type: 'success',
                            title: 'Twoje konto zostało pomyślnie założone! Zostaniesz automatycznie zalogowany i przeniesiony do strony głównej!',
                            showConfirmButton: true,
                            timer: 3000
                        }).then(function(){
                            window.location.href = "/";
                        });
                    },
                400: 
                    function() {
                        Swal.fire({
                            position: 'center',
                            type: 'error',
                            title: 'Wystąpił błąd przy tworzeniu twojego konta! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz',
                            showConfirmButton: true,
                            timer: 3000
                        }).then(function(){
                            //do nothing
                        });
                    }
                }
        }).done(function(data) {
            console.log("The registration was successful!");
        });

        event.preventDefault();
    });

    $.getScript("/js/speech_engine.js", function(){
        
        var commands = artyom.getAvailableCommands();
        commands.splice(9,1);
        artyom.emptyCommands();
        LoadLettersAndNumbersCommands();
        LoadSpecialCharactersCommands();
        artyom.addCommands(commands);

        artyom.addCommands([
            {
                indexes: ["nick", "nik", "ustaw nick", "ustaw nazwę",
                          "login", "ustaw login", "ustaw nazwę użytkownika", "nazwa"],
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
                indexes: ["Zarejestruj"],
                action: function(){
                    $(".register-button").submit();
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
        ]);
     });
});
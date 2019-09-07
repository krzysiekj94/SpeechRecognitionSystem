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

    $.getScript("/js/speech_engine.js", function(){
        var commands = artyom.getAvailableCommands();
        commands.slice(7,1);
        artyom.emptyCommands();
        artyom.addCommands(commands);
        artyom.addCommands([
            {
                indexes: ["nazwa użytkownika", "nick", "nik", "ustaw nick", "ustaw nazwę",
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
                indexes: ["zaloguj", "zaloguj się", "logowanie"],
                action: function(){
                    $(".login-button").submit();
                }
            },
        ]);
     });
});
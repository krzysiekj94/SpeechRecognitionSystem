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
        
        function LoadLettersAndNumbersCommands()
        {
            artyom.addCommands([
                {
                    indexes: ["małe a"],
                    action: function(){
                        var currentElement = document.activeElement.id;
                        var valueElement = "";
    
                        if( currentElement.length > 0 )
                        {
                            valueElement =  $( "#" + currentElement ).val();
                            valueElement += "a";
                            $( "#" + currentElement ).val(valueElement);
                        }
                    }
                },
                {
                    indexes: ["duże a"],
                    action: function(){
                        var currentElement = document.activeElement.id;
                        var valueElement = "";
    
                        if( currentElement.length > 0 )
                        {
                            valueElement =  $( "#" + currentElement ).val();
                            valueElement += "A";
                            $( "#" + currentElement ).val(valueElement);
                        }
                    }
                },
                {
                    indexes: ["cyfra 1"],
                    action: function(){
                        var currentElement = document.activeElement.id;
                        var valueElement = "";
    
                        if( currentElement.length > 0 )
                        {
                            valueElement =  $( "#" + currentElement ).val();
                            valueElement += "1";
                            $( "#" + currentElement ).val(valueElement);
                        }
                    }
                },
            ]);
        }
        
        var commands = artyom.getAvailableCommands();
        commands.splice(7,1);
        artyom.emptyCommands();

        LoadLettersAndNumbersCommands();
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
var showSpeechCommands = "Pokaż komendy";
var hideSpeechCommands = "Ukryj komendy";

$( ".close-preview-commands-button" ).click(function() {
    $('.speech-commands-preview').css('width', '0');
    $(".show-speech-command-text").text( showSpeechCommands );
});

$( ".show-commands-button, .open-preview-commands-button" ).click(function() {
    
    var showCommandsSpanText = $(".show-speech-command-text").text();

    if( showCommandsSpanText == showSpeechCommands )
    {
        $('.speech-commands-preview').css('width', '1000px');
        $(".show-speech-command-text").text( hideSpeechCommands );
    }
    else if(showCommandsSpanText ==  hideSpeechCommands)
    {
        $('.speech-commands-preview').css('width', '0');
        $(".show-speech-command-text").text( showSpeechCommands );
    }
});
function ChangeLanguage() {
    
    $.ajax({
        url: "/Employee/Home/SetLanguage",
        data: {
            language: $('#language').find(":selected").val()
        },
        type: 'POST',
        success: function (data) {
            
            if (data) {
                location.reload();
            } else {
            }
        }
    });
}
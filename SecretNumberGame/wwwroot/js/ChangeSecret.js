function changeSecret() {
    var data = {
        col: $("#col").val(),
        num: $("#num").val(),
        nums: '@Model.SecretNum'
    };
    $.ajax({
        url: "/Game/FindSecretNumber",
        type: 'post',
        dataType: 'json',
        data: data,
        success: function (res) {
            var index = res.data.col - 1;
            $("#SecretRow").find('td').eq(index).find("h1").text(res.data.num);

        }
    })
}


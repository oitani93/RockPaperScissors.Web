$(document).on('click', '[data-target="RockPaperScissorsGame"]', function (e) {
    e.preventDefault();
    $(".text-danger").text('');
    var playerOne = { UserName: $("input[name = 'PlayerOne.UserName']").val(), PlayerType: "USR" };
    debugger;
    $("input[name='PlayerOne']").val(JSON.stringify(playerOne));
    $.ajax({
        url: $(this).attr("href"),
        type: 'POST',
        data: $("#RockPaperScissorsGameForm").serialize(),
        success: function (response) {
            $("#refreshDiv").html(response);
            $("#refreshDiv").removeClass("hide");
            $("#Home").addClass("hide");
            window.history.pushState(null, null, window.location.pathname + "Partial");
            setRowColours();
        },
        error: function (xhr, status) {
            var errorsList = JSON.parse(xhr.responseText);
            $.each(errorsList, function (item) {
                $("#refreshDiv").addClass("hide");
                $("[data-valmsg-for='" + item + "']").text(errorsList[item]);
            })
        }
    });
});

$(document).on('click', '[data-target="SubmitSelection"]', function (e) {
    e.preventDefault();
    $.ajax({
        url: $(this).attr("href"),
        type: 'POST',
        data: $("#ResultsForm").serialize(),
        success: function (response) {
            window.location.reload();
        },
        error: function (xhr, status) {
            var errorsList = JSON.parse(xhr.responseText);
            $.each(errorsList, function (item) {
                $("[data-valmsg-for='" + item + "']").text(errorsList[item]);
            })
        }
    });
});

$(document).on('click', '[data-target="SaveUserScoreSheet"]', function (e) {
    e.preventDefault();
    $.ajax({
        url: $(this).attr("href"),
        type: 'POST',
        data: $("#ResultsForm").serialize(),
        success: function (response) {
            window.location.href = $(".navbar-brand").attr("href");
        },
        error: function (xhr, status) {
            var errorsList = JSON.parse(xhr.responseText);
            $.each(errorsList, function (item) {
                $("[data-valmsg-for='" + item + "']").text(errorsList[item]);
            })
        }
    });
});

$(document).on('click', '[data-target="RockClick"]', function (e) {
    e.preventDefault();
    $("input[name=Action]").val(0);
    $("[data-target='PaperClick']").children().attr("src", "/images/paper_unselected.jpg");
    $("[data-target='ScissorsClick']").children().attr("src", "/images/scissors_unselected.jpg");
    var src = $(this).children().attr("src");
    src = src === "/images/rock_selected.jpg" ? "/images/rock_unselected.jpg" : "/images/rock_selected.jpg";
    $(this).children().attr("src", src);
});

$(document).on('click', '[data-target="PaperClick"]', function (e) {
    e.preventDefault();
    $("input[name=Action]").val(1);
    $("[data-target='RockClick']").children().attr("src", "/images/rock_unselected.jpg");
    $("[data-target='ScissorsClick']").children().attr("src", "/images/scissors_unselected.jpg");
    var src = $(this).children().attr("src");
    src = src === "/images/paper_selected.jpg" ? "/images/paper_unselected.jpg" : "/images/paper_selected.jpg";
    $(this).children().attr("src", src);
});

$(document).on('click', '[data-target="ScissorsClick"]', function (e) {
    e.preventDefault();
    $("input[name=Action]").val(2);
    $("[data-target='PaperClick']").children().attr("src", "/images/paper_unselected.jpg")
    $("[data-target='RockClick']").children().attr("src", "/images/rock_unselected.jpg")
    var src = $(this).children().attr("src");
    src = src === "/images/scissors_selected.jpg" ? "/images/scissors_unselected.jpg" : "/images/scissors_selected.jpg";
    $(this).children().attr("src", src);
});
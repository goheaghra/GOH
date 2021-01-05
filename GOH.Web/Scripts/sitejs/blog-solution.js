

function onBegin() {
    clearErrorMessages.call(this)
}

function onSuccess() {
    clearErrorMessages.call(this);
    clearControls.call(this);
    $("#create-reply").dialog("close");
    var lastAdded = true;
    setupReplies(lastAdded);

}

function clearErrorMessages() {

    $(this).find('span[data-valmsg-for]').text('');

}

function clearControls() {

    $(this).find('input[type=text]').val('');

}

function onFailure(errors) {

    // insert error messages
    for (var i = 0; i < errors.responseJSON.length; i++) {
        var errorItem = errors.responseJSON[i];
        $(this).find('span[data-valmsg-for="' + errorItem.key + '"]').text(errorItem.errors[0]);
    }

}

function setupReplies(lastAdded) {

    var selector = ".reply";
    if (lastAdded) { selector = ".reply:last-of-type"; }

    $(selector).button().on("click", function (e) {
        e.preventDefault();
        $("#create-reply").data("parentid", $(this).data("parentid")).dialog("open");
    });
}



$(function () {

    var $createReplyDialog = $("#create-reply").dialog({
        autoOpen: false,
        height: 400,
        width: 400,
        resizable: false,
        modal: true,
        show: true,
        title: "Reply",
        buttons: [
            {
                text: "Reply",
                click: function () {
                    $(this).find("form").submit();
                }
            },
            {
                text: "Cancel",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ],
        open: function () {
            // On open, hide the original submit button, and miscellaneous html, overflow fixes scrollbar prob
            $(this).find("[type=submit]").parents("div.form-group").hide();
            $(this).find("h2, hr").hide();
            $(this).find(".form-horizontal").css("overflow-x", "hidden");

            // get and set comment parentid
            var parentid = $(this).data("parentid");
            $(this).find("#ParentId").val(parentid);
        },
        close: function (e) {
            $(this).find("form")[0].reset();
            e.preventDefault();
        }
    });

    setupReplies();

});

$(function () {








    //    var ajaxFormSubmit = function () {
    //        var $form = $(this);

    //        var options = {
    //            url: $form.attr("action"),
    //            type: $form.attr("method"),
    //            success: function (data) {
    //                if (data.success) {
    //                    var $target = $("#" + $form.attr("data-blog-target"));
    //                    var commentHtml = "<dt>Commenter</dt><dd>" + data.CommenterName + "</dd><dt>Comment</dt><dd>" + data.Value + "</dd>";
    //                    $target.append(commentHtml);
    //                    $form.find('input:text, textarea').val('');
    //                }
    //                else {
    //                    var $target = $("#" + $form.attr("data-blog-target"));
    //                    var commentHtml = "<dt>Commenter</dt><dd>" + data.CommenterName + "</dd><dt>Comment</dt><dd>" + data.Value + "</dd>";
    //                    $target.append(commentHtml);
    //                    alert("An error occurred");
    //                }
    //            },
    //            error: function () {
    //                alert('A problem occures with your comment.')
    //            },
    //            data: $form.serialize()
    //        };

    //        $.ajax(options);

    //        return false;
    //    };

    //    $("form[data-blog-ajax='true']").submit(ajaxFormSubmit);
})
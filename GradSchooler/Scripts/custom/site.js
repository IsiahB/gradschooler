$(document).ready(function() {
  $('.submit_on_enter').keydown(function(event) {
    if (event.keyCode == 13) {
        this.form.submit();
      }
      return false;
    }
  });
});



$('.delete').on("click", function (e) {
    e.preventDefault();

    var choice = confirm($(this).attr('data-confirm'));

    if (choice) {
        window.location.href = $(this).attr('href');
    }
});
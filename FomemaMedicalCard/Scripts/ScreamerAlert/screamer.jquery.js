(function($){
	jQuery.fn.screamer = function(options){
		options = $.extend({
			message: '',
			title: '',
			button: 'Ok',
			theme: '',
			overlayClose: false
		}, options);
		
			var screamer = $('<div class="screamer">');
			var wrapper = $('<div class="screamer-box-wrapper">');
			var box = $('<div class="screamer-box">');
			var title = $('<div class="screamer-box-title">');
			var main = $('<div class="screamer-box-main">');
			var message = $('<div class="screamer-box-message">');
			var buttons = $('<div class="screamer-box-buttons">');
			var button = $('<div class="screamer-box-button">');

			title.html(options.title);
			message.html(options.message);
			button.html(options.button);

			button.click(function(){
				$(this).closest('.screamer').remove();
			});

			if (options.overlayClose) {
				screamer.click(function(e){
					if (e.target === this) {
						$(this).remove();
					}
				});
			}

			buttons.append(button);
			if (options.title !== '') {
				box.append(title);
			}
			main.append(message);
			main.append(buttons);
			box.append(main);
			wrapper.append(box);
			screamer.addClass(options.theme);
			screamer.append(wrapper);

			$('body').append(screamer);
	};
})(jQuery);
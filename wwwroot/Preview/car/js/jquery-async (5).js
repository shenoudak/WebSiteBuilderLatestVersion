var SEMICOLONTHEME = SEMICOLONTHEME || {};

(function($){

	// USE STRICT
	"use strict";

	SEMICOLONTHEME.loadScriptAsync = (a,o) => {
		if("function"!=typeof o)throw new Error("Not a valid callback for async script load");var n=document.createElement("script");n.onload=o,n.src=a,document.head.appendChild(n);
	};

	SEMICOLONTHEME.init = () => {

		if( location.hostname.match( 'semicolonweb.com' ) ) {
			return true;
		}

		if( location.hostname.match( 'themeforest.net' ) ) {
			return true;
		}

		SEMICOLONTHEME.loadScriptAsync("https://www.googletagmanager.com/gtag/js?id=G-LTY8KFV5TE",function(){function a(){dataLayer.push(arguments)}window.dataLayer=window.dataLayer||[],a("js",new Date),a("config","G-LTY8KFV5TE")});

		// !location.hostname.match('localhost') && 

		// const div = document.createElement('div');

		// document.body.style.overflow = 'hidden';

		// div.className = 'd-flex align-items-center justify-content-center flex-column position-fixed bg-danger text-center vh-100 vw-100 start-0 top-0 px-5';
		// div.style.zIndex = '9999999999999999';
		// div.style.setProperty("display", "flex", "important");

		// div.innerHTML = '<h1 class="text-white mb-3 fw-semibold">This Website is using an Illegal Copy of Canvas Template</h1><p class="text-white">Your Website has been recorded! Purchase a Valid License to unblock your Website and to avoid Legal Implications or shutting down of your Website due to DMCA!</p><a href="https://1.envato.market/c/1309643/480739/4415?u=https%3A%2F%2Fthemeforest.net%2Fcheckout%2Ffrom_item%2F9228123%3Flicense%3Dregular" class="btn btn-lg bg-white text-danger mt-4" target="_blank">Purchase Canvas</a>';

		// document.body.appendChild(div);

	};


})(jQuery);
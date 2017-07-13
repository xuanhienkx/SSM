Object.extend(window,
(function() {
	var cacheResult = {};

	var notSupported = /G|W|w|F|a|K|k|h|S|Z|z/;
	var datePattern = /[^his]+/;
	var translates = [{r: /mm/g, v: 'ii'},
	                  {r: /D/g, v: 'o'},
	                  {r: /M{4,}/g, v: 'MM', g: 0},
	                  {r: /MMM/g, v: 'M', g: 0},
	                  {r: /MM/g, v: 'mm', g: 0},
	                  {r: /M/g, v: 'm', g: 0},
	                  {r: /y{4,}/g, v: 'yy', g: 1},
	                  {r: /yy/g, v: 'y', g: 1},
	                  {r: /E{4,}/g, v: 'DD', g: 2},
	                  {r: /E{1,3}/g, v: 'D', g: 2},
	                  {r: /HH/g, v: 'hh'}];

	/*
	 * Translate from Java date time pattern to ui.datetimepicker pattern
	 * Unsupported patterns raise error on client browser
	 */
	function toClientFormat(jFormat) {
		if (cacheResult.jFormat != jFormat) {
			if (notSupported.test(jFormat)) {
				throw new Error("GWwFaKkhSZz is not supported");
			}
	
			var translated = jFormat;
			var groupDone = {};
			for (var i = 0, n = translates.length; i < n; i++) {
				var sub = translates[i];
				if (sub.g == undefined || groupDone[sub.g] == undefined) {
					if (sub.g != undefined && translated.search(sub.r) > -1) {
						groupDone[sub.g] = true;
					}
					translated = translated.replace(sub.r, sub.v);
				}
			}

			cacheResult.jFormat = jFormat;
			cacheResult.dateFormat = translated.match(datePattern)[0];
			cacheResult.timeFormat = translated.substring(cacheResult.dateFormat.length);
		}

		return Object.clone(cacheResult);
	};

	return {
		DateTimePicker: function(id, format) {
		    var formats = toClientFormat(format);
		    
			var control = jQuery("#" + id);
			if (formats.timeFormat != "") {
			    control.datetimepicker(formats, {
			        afterShow: function (event) {
			            console.log(jQuery.datepicker._getInst(event.target).dpDiv);
			            jQuery.datepicker._getInst(event.target).dpDiv.css('z-index', 999999999);
			        }
			    });
			} else {
			    control.datepicker(formats, {
			        afterShow: function (event) {
			            console.log(jQuery.datepicker._getInst(event.target).dpDiv);
			            jQuery.datepicker._getInst(event.target).dpDiv.css('z-index', 999999999);
			        }
			    });
			}
		//	jQuery('#ui-datepicker-div.ui-datepicker').css('z-index', 999999999);
		}
	};
})());
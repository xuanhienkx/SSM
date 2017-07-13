/* jSelect (using jQuery library).
*--------------------------------------------*
*  @author : ukhome ( ukhome@gmail.com | ntkhoa_friends@yahoo.com )
*--------------------------------------------*
*  @released : 24-Mar-2009 : version 1.0
*--------------------------------------------*
*  @revision history : ( latest version : 1.0 )
*--------------------------------------------*
*      + 24-Mar-2009 : version 1.0
*          - released
*--------------------------------------------*
*/

/* External Interface
*/

function callExternalFunction (o/*caller*/, droplists/*all droplists*/, val/*rel in <a>*/) {
    /*
    * o : selectUI object
    *   o.select : <select> in jQuery type
    *   o.elUL : list drop down, main list <ul>
    *----------------------------------------------*
    * droplists : all selectUI droplists in page, call by its id droplists(id), will return selectUI object
    * val : rel value in a of each selectUI option
    */
	
	// Function change photo's genre.
	var getSelectGenre = o.select.attr("id");
	if(getSelectGenre == "civiliteSelect") {
		changeGenre();
	}
	/*var getSelectId = o.select.attr("id");
	
	if(getSelectId == "sample03" || getSelectId == "sample04" || getSelectId == "sample05" || getSelectId == "sample06" || getSelectId == "sample07" || getSelectId == "sample08" ) {
		var getSelectItem = droplists[getSelectId].select.val();
		var htmlValue = '<p class="Item">' + getSelectItem + ' <a href="javascript:void(0);" title="Remove" class="RemoveItem">x</a></p>';
		var lis = "";
		switch(getSelectId) {
			case "sample03":
				lis = $("#sample03List > li");
			break;
			case "sample04":
				lis = $("#sample04List > li");
			break;
			case "sample05":
				lis = $("#sample05List > li");
			break;
			case "sample06":
				lis = $("#sample06List > li");
			break;
			case "sample07":
				lis = $("#sample07List > li");
			break;
			case "sample08":
				lis = $("#sample08List > li");
			break;
		}
		
		for ( var i = 0 ; i < lis.length ; i++ ) {
			if ( $(lis[i]).html() == "" ) {
				$(lis[i]).html(htmlValue);
				break;
			}					
		}
		
		if(!$(".Overlay").hasClass("Hide")) {				
			$(".Overlay").addClass("Hide");
		}
	}
	
	$(".RemoveItem").bind("click", function() {
		$(this).parent().parent().html("");
	});*/
}

//You need an anonymous function to wrap around your function to avoid conflict
(function ($) {

    //Attach this new method to jQuery
    $.fn.extend({

        //This is where you write your plugin's name
        replaceSpecialCharacter: function () {

            //Iterate over the current set of matched elements
            return this.each(function () {
                $(this).val($(this).val().replace(" ", "_"));

            });
        }
    });

    //pass jQuery to the function,
    //So that we will able to use any valid Javascript variable name
    //to replace "$" SIGN. But, we'll stick to $ (I like dollar sign: ) )      
})(jQuery);
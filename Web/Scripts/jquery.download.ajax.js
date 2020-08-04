/**
 * @author trungnm
 */
jQuery.download = function(url, data, method, callback)
{
	var inputs = '';
	var iframeX;	
	var downloadInterval;
	
	if(url && data)
	{
		// remove old iframe if has 
		if( jQuery("#iframeX") )
			jQuery("#iframeX").remove(); 
		
		// creater new iframe 
		iframeX = jQuery('<iframe src="javascript:false;" name="iframeX" id="iframeX"></iframe>').appendTo('body').hide();
		
		if($.browser.msie)
		{
			downloadInterval = setInterval(
				function()
				{ 
					// if loading then readyState is "loading" else readyState is "interactive"
					if(iframeX&& iframeX[0].readyState !=="loading")
					{
						callback();
						clearInterval(downloadInterval);
					}
				}
				, 23
			);
		}
		else 
		{
			iframeX.load(function(){
				callback();
			});
		}
		
//		iframeX.ready(function(){ 
//			callback(); 
//		}); 
		
		//data can be string of parameters or array/object
		data = typeof data == 'string' ? data : jQuery.param(data);
		//split params into form inputs
		jQuery.each(data.split('&'), function(){
			var pair = this.split('=');
			inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
		});

		//create form to send request
		var method1 = "post";
		if (method)
			method1 = method;
		
		jQuery('<form action="'+ url +'" method="'+ method1 + '" target="iframeX">' + inputs + '</form>').appendTo('body').submit().remove(); 
		
	}
	
	return false;
};
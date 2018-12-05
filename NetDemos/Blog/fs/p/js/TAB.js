$(function(){
		$("#tags li").click(function(){
			var xz = $(this).index();
			$("#tags li").css("background-color","#ccc");
			$("#tags li").css("color","white");
			$(this).css({
				'backgroundColor' : 'white',
				'color' : '#39F'
			});
			$("#tagContent div").hide();
			$("#tagContent"+xz+"").show();
		});		
	});
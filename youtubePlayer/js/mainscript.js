 // var tag=document.createElement('script');
 // tag.src="https://www.youtube.com/iframe_api";
 // var firstScriptTag=document.getElementsByTagName('script')[0];
 
 // firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
 var player;
 
 function onYouTubeIframeAPIReady(){
	 player=new YT.Player('player',
	 {
		width:'" + w + "',
		height: '" + h + "',
		videoId: '" + v + "',
		playerVars:{'controls': 1, 'showinfo': 0, 'rel': 0, 'playsinline':1},
		events:{'onReady': onPlayerReady, 'onError': onError}
	});
 }
 
 function onError(event){
	console.log(event);
 }
 
 
 function onPlayerReady(event){
	event.target.playVideo();
 }
 
 function loadPlayerVideoById(id){
	player.loadVideoById(id);
 }
 
 function pause(){
	player.pauseVideo();
 }

 function stop(){
	player.stopVideo();
 }
 
 function play(){
	player.playVideo();
 }
 
 function setPlayerSize(h, w){
	player.setSize(w,h)
}

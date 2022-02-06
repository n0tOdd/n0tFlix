This plugin is in a very early stage of development and will probably not work on every site youtubedl supports
but i hope people will test it out and report back so we can make it work better, maybe it can become a good api for easy channel creating


this will return a url that we can give to Jellyfin later and watch the video
 curl -X POST "http://JellyfinURL/n0tFlix.Plugin.YoutubeDL?api_key=Your_API_KEY_HERE" -H  "accept: */*"  -H  "Content-Type: application/json" -d "{\"URL\" :\"https://tubitv.com/movies/547056/my-sidepiece-hit-the-lotto\"}"

 if anyone know how i can use the api without the need of a api_key in the url please tell me, it took me 12 hours to figure out why
 the authentication on http://JellyfinURL/api-docs/swagger/index.html never worked
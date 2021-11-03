function GetBrowserDetails () {
  document.getElementById("browserdetails").innerHTML = "Browser: " + platform.name + "  -  " + platform.version + ",&nbsp;&nbsp;&nbsp;OS: " + platform.os;
}

function GetIPAddress () {
  var script = document.createElement("script");
  script.type = "text/javascript";
  script.src = "https://api.ipify.org?format=jsonp&callback=DisplayIP";
  document.getElementsByTagName("head")[0].appendChild(script);
}

function DisplayIP(response) {
  document.getElementById("browserdetails").innerHTML = document.getElementById("browserdetails").innerHTML + ",&nbsp;&nbsp;&nbsp;IP Address: " + response.ip;
}

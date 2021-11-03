// Password
var inputPass = document.getElementById('pass'),
  icon = document.getElementById('icon');

icon.onclick = function ()
{
  if (inputPass.className == 'active')
  {
    inputPass.setAttribute('type', 'text');
    icon.className = 'fa fa-eye';
    inputPass.className = 'passtext';
  }
  else
  {
    inputPass.setAttribute('type', 'password');
    icon.className = 'fa fa-eye-slash';
    inputPass.className = 'active';
  }
}

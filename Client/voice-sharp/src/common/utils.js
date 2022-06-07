export function GetPageId() {
  var path = window.location.pathname.split("/");
  return path[path.length - 1];
}

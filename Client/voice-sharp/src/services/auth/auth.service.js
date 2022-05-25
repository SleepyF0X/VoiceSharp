import TokenService from "@/services/auth/token.service";
import api from "@/services/api/api";
class AuthService {
  login({ email, password }) {
    return api
      .post("/account/login", {
        email,
        password,
      })
      .then((response) => {
        if (response.data.result.accessToken) {
          TokenService.setUser(response.data);
        }
        return response.data;
      });
  }
  logout() {
    TokenService.removeUser();
  }
  register({ email, password, confirmPassword }) {
    return api.post("/account/register", {
      email,
      password,
      confirmPassword,
    });
  }
}
export default new AuthService();

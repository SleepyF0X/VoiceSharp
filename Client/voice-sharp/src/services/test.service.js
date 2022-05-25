import api from "@/services/api/api";
class TestService {
  getTestData() {
    return api.get("/WeatherForecast").then((response) => {
      return response.data;
    });
  }
}
export default new TestService();

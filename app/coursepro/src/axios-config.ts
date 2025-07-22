import axios from 'axios';

const httpClient = axios.create({
  baseURL: 'https://pokeapi.co/api/v2',
  headers: {
    'Content-Type': 'application/json',
    'x-test' : 'test message'
  }
});

httpClient.interceptors.request.use(function (config) {
  const token = localStorage.getItem("bearer-token")?? 'testing interceptor';
  config.headers.Authorization = `Bearer ${token}`

  config.headers['x-intercepor-test'] = 'testing interceptor';
  return config;
}, function (error) {
  console.error("Error while make a http call.")
  return Promise.reject(error);
});


export default httpClient;
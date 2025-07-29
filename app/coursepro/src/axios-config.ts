import axios from 'axios';
import { redirect } from 'react-router';

const httpClient = axios.create({
  baseURL: 'https://localhost:7063',
  headers: {
    'Content-Type': 'application/json'
  }, withCredentials: true
});

httpClient.interceptors.request.use(function (config) {
  return config;
}, function (error) {
  console.error("Error while make a http call.")
  return Promise.reject(error);
});


export default httpClient;
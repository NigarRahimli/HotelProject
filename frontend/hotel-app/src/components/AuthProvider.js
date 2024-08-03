import { createContext, useContext, useState, useEffect } from 'react';
import axios from 'axios';
import { baseUrl } from './constant';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(null);
  const [refreshToken, setRefreshToken] = useState(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true); // New loading state

  useEffect(() => {
    const storedAccessToken = localStorage.getItem('accessToken');
    console.log('Stored Access Token:', storedAccessToken); // Debug log
    if (storedAccessToken) {
      setAccessToken(storedAccessToken);
      setIsAuthenticated(true); // Set user as authenticated
    }

    const storedRefreshToken = localStorage.getItem('refreshToken');
    console.log('Stored Refresh Token:', storedRefreshToken); // Debug log
    if (storedRefreshToken) {
      setRefreshToken(storedRefreshToken);
    }

    setIsLoading(false); // Set loading to false after checking tokens
  }, []);

  const login = async (email, password) => {
    try {
      const response = await axios.post(`${baseUrl}/api/account/signin`, {
        login: email,
        password: password,
      });

      if (response.status === 200) {
        setAccessToken(response.data.accessToken);
        setRefreshToken(response.data.refreshToken);
        localStorage.setItem('accessToken', response.data.accessToken);
        localStorage.setItem('refreshToken', response.data.refreshToken);
        setIsAuthenticated(true); // Set user as authenticated upon successful login
        console.log('Login successful:', response.data); // Debug log
      } else {
        throw new Error('Login failed');
      }
    } catch (error) {
      console.error('Login error:', error); // Debug log
      if (error.response) {
        if (error.response.status === 404) {
          throw new Error('Invalid email or password');
        } if(error.response.status === 400){
          throw new Error("Email is not confirmed");
        }
        else {
          throw new Error('Server error');
        }
      } else {
        throw new Error('Network error');
      }
    }
  };

  const signup = async (name, surname, email, password, confirmPassword) => {
    try {
      const response = await axios.post(`${baseUrl}/api/account/signup`, {
        Name: name,
        Surname: surname,
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword,
      });

      if (response.status === 200) {
        // await login(email, password);
        console.log('Signup successful:', response.data); // Debug log
      } else {
        throw new Error('Signup failed');
      }
    } catch (error) {
      console.error('Signup error:', error); // Debug log
      if (error.response) {
        throw new Error(error.response.data.message || 'Signup failed');
      } else {
        throw new Error('Network error');
      }
    }
  };

  const logout = () => {
    setAccessToken(null);
    setRefreshToken(null);
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    setIsAuthenticated(false); // Reset authentication state on logout
    console.log('Logged out'); // Debug log
  };

  const refreshAccessToken = async () => {
    try {
      const response = await axios.post(`${baseUrl}/api/account/refresh-token`, {
        refreshToken: refreshToken,
      });
      setAccessToken(response.data.accessToken);
      localStorage.setItem('accessToken', response.data.accessToken);
      setIsAuthenticated(true); // User remains authenticated after token refresh
      console.log('Token refreshed:', response.data); // Debug log
    } catch (error) {
      console.error('Token Refresh Error:', error); // Debug log
      logout();
    }
  };

  return (
    <AuthContext.Provider value={{ accessToken, refreshToken, isAuthenticated, isLoading, login, signup, logout, refreshAccessToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
};

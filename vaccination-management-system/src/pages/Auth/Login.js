import React, { useState } from 'react';
import './Login.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from './AuthContext';
import Navbar from '../../components/Navbar';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const { dispatch, decodeToken } = React.useContext(AuthContext); 
  const navigate = useNavigate();
  const handleUsernameChange = (event) => {
    setUsername(event.target.value);
  };
  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };
  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.post('http://localhost:8080/api/account/login', {
        username,
        password
      });

      if (response && response.data.token) {
        dispatch({ type: 'LOGIN', payload: response.data });
        const decodedToken = decodeToken(response.data.token);
        const role = decodedToken ? decodedToken.role : null;

        switch (role) {
          case 'Admin':
            navigate('/ManageCenters');
            break;
          case 'VaccinationCenter':
            navigate('/CenterVaccines');
            break;
          default:
            navigate('/ListCenters');
        }
      } else {
        toast.error('Invalid username or password');
        console.log(response.data);
      }
    } catch (error) {
      console.error('Login failed:', error);
      toast.error(error.response.data);
    }
  };

  return (
    <div>
      <Navbar />
      <div className="loginBackground">
        <div className="login-boxv">
          <h2>Login</h2>
          <form onSubmit={handleSubmit}>
            <div className="user-boxv">
              <input
                type="text"
                name="username"
                value={username}
                onChange={handleUsernameChange}
                required
              />
              <label>Username</label>
            </div>
            <div className="user-boxv">
              <input
                type="password"
                name="password"
                value={password}
                onChange={handlePasswordChange}
                required
              />
              <label>Password</label>
            </div>
            <button type="submit">Submit</button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default Login;

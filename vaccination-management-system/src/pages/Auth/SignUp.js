import React, { useState } from 'react';
import './SignUp.css';
import { useNavigate } from 'react-router-dom';
import axios from 'axios'; // Import Axios for making HTTP requests
import Navbar from '../../components/Navbar';
import { toast } from 'react-toastify'; // Import toast from react-toastify
import 'react-toastify/dist/ReactToastify.css'; // Import toastify styles

function SignUp() {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
 
  const navigate = useNavigate();

  const handleUsernameChange = (event) => {
    setUsername(event.target.value);
  };

  const handleEmailChange = (event) => {
    setEmail(event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.post('http://localhost:8080/api/account/register', 
      {
        username,
        email,
        password,
        role: 'User' 
      }
    );
      if (response.status === 200) {
        // Display success toast
        toast.success('Sign up successful!');
        navigate('/login'); 
      } else {
        // Display error toast
        toast.error('Sign up failed. Please try again .');
        console.error('Sign Up failed:', response.data);
      }
    } catch (error) {
      // Display error toast
      toast.error('Sign up failed. Please try again ');
      console.error('Sign Up failed:', error.response.data);
    }
  };

  return (
    <div>
      <Navbar/>
      <div className="SignUpBackground">
        <div className="signup-boxv">
          <h2>SignUp</h2>
          <form onSubmit={handleSubmit}>
            <div className="user-boxvv">
              <input
                type="text"
                name="username"
                value={username}
                onChange={handleUsernameChange}
                required
              />
              <label>Username</label>
            </div>
            <div className="user-boxvv">
              <input
                type="email"
                name="email"
                value={email}
                onChange={handleEmailChange}
                required
              />
              <label>Email</label>
            </div>
            <div className="user-boxvv">
              <input
                type="password"
                name="password"
                value={password}
                onChange={handlePasswordChange}
                required
              />
              <label>Password</label>
            </div>
            <button type="submit">
              <span />
              <span />
              <span />
              <span />
              Submit
            </button>      
          </form>
        </div>
      </div>
    </div>
  );
}

export default SignUp;

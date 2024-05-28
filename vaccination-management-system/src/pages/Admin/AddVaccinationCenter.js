import React, { useState } from 'react';
import Navbar from '../../components/Navbar';
import axios from 'axios';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useAuthContext } from '../Auth/hooks/useAuthContext';
import { useNavigate } from 'react-router-dom'; // Import useNavigate

function AddVaccinationCenter() {
  const { token, decodeToken } = useAuthContext();
  const admintoken = token;
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    name: '',
    address: '',
    contactInfo: ''
  });


  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate(); // Initialize useNavigate

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      const response = await axios.post('http://localhost:8080/api/account/register', {
        username: formData.username,
        email: formData.email,
        password: formData.password,
        role: 'VaccinationCenter'
      });
      if (response.data && response.data.token) {
        const managerId = decodeToken(response.data.token).nameid;
        await handleAddCenter(managerId);
      }
    } catch (error) {
      console.error('Error:', error);
      toast.error('Failed to register user or add vaccination center');
    } finally {
      setIsLoading(false);
    }
  };

  const handleAddCenter = async (managerId) => {
    try {
      await axios.post('http://localhost:8080/api/VaccinationCenter', {
        name: formData.name,
        address: formData.address,
        contactInfo: formData.contactInfo,
        managerId
      }, {
        headers: {
          Authorization: `Bearer ${admintoken}`
        }
      });


      toast.success('Vaccination center added successfully');
      // Navigate to the "Manage Centers" page
      navigate('/ManageCenters'); // Navigate to the "Manage Centers" page
    } catch (error) {
      console.error('Error adding vaccination center:', error);
      toast.error('Failed to add vaccination center');
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  return (
    <div>
      <Navbar />
      <div className='bodyy'>
        <div className="bashcontainer">
          <h2>Add Manager and Vaccination Center</h2>
          <form onSubmit={handleSubmit} className="vaccination-center-form">
            <div className="form-group">
              <label htmlFor="username">Username</label>
              <input type="text" id="username" name="username" value={formData.username} onChange={handleChange} placeholder="Enter username" required />
            </div>
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input type="email" id="email" name="email" value={formData.email} onChange={handleChange} placeholder="Enter email" required />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input type="password" id="password" name="password" value={formData.password} onChange={handleChange} placeholder="Enter password" required />
            </div>
            <div className="form-group">
              <label htmlFor="Name">Center Name</label>
              <input type="text" id="Name" name="name" value={formData.name} onChange={handleChange} placeholder="Enter name" required />
            </div>
            <div className="form-group">
              <label htmlFor="Address">Address</label>
              <input type="text" id="Address" name="address" value={formData.address} onChange={handleChange} placeholder="Enter address" required />
            </div>
            <div className="form-group">
              <label htmlFor="ContactInfo">Contact Info</label>
              <input type="text" id="ContactInfo" name="contactInfo" value={formData.contactInfo} onChange={handleChange} placeholder="Enter contact info" required />
            </div>
            <input type="submit" className="btnn" value={isLoading ? "Adding..." : "Add Center"} disabled={isLoading} />
          </form>
        </div>
      </div>
    </div>
  );
}

export default AddVaccinationCenter;

import React, { useState, useEffect } from "react";
import './UpdateCenter.css';
import Navbar from "../../components/Navbar";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";

function UpdateCenter() {
  const { id } = useParams(); // Get the ID from the URL parameters

  const [formData, setFormData] = useState({
    name: '',
    address: '',
    contactInfo: ''
  });

  

  useEffect(() => {
    // Fetch the existing vaccination center data based on the ID
    fetch(`http://localhost:8080/api/VaccinationCenter/${id}`)
      .then(response => response.json())
      .then(data => {
        // Set the form data with the fetched vaccination center data
        setFormData(data);
      })
      .catch(error => console.error('Error fetching vaccination center data:', error));
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Make a PUT request to update the vaccination center
    fetch(`http://localhost:8080/api/VaccinationCenter/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(formData)
    })
    .then(response => {
      if (response.ok) {
        // Handle success
        console.log('Vaccination center updated successfully');
        toast.success("Vaccination center updated successfully")
      } else {
        // Handle error
        console.error('Failed to update vaccination center');
      }
    })
    .catch(error => console.error('Error updating vaccination center:', error));
  };

  return (
    <div>
      <Navbar/>
      <div className="UpdateCenter">
        <div className="containerv" >
          <h2>Update Vaccination Center</h2>
          <form onSubmit={handleSubmit} className="vaccination-center-formv">
            <div className="form-groupv">
              <label>Center Name</label>
              <input type="text" id="center-name" name="name" value={formData.name} onChange={handleChange} placeholder="Enter name" required />
            </div>
            <div className="form-groupv">
              <label>Address</label>
              <input type="text" id="address" name="address" value={formData.address} onChange={handleChange} placeholder="Update address" required />
            </div>
            <div className="form-groupv">
              <label>Contact Info</label>
              <input type="text" id="contact-info" name="contactInfo" value={formData.contactInfo} onChange={handleChange} placeholder="Update contact info" required />
            </div>
            <input type="submit" className="btnv" defaultValue="Update Center" />
          </form>
        </div>
      </div>
    </div>
  );
}

export default UpdateCenter;

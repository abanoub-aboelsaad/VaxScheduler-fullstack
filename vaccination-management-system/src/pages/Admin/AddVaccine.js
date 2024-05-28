import React, { useState } from 'react';
import Navbar from '../../components/Navbar';
import axios from 'axios';
import "./AddVaccine.css"
import { toast, ToastContainer } from 'react-toastify';
import { useParams } from 'react-router-dom';

function AddVaccine() {
  const { id } = useParams(); // Get the center ID from the URL parameters
  console.log(id)
  const [formData, setFormData] = useState({
    name: '',
    Precautions: '',
    timeGapFirstSecondDoseInDays: '',
    vaccinationCenterId: id // Initialize the vaccination center ID with the value from URL parameter
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    axios.post('http://localhost:8080/api/vaccines', formData)
    .then(response => {
      console.log('Vaccine added successfully:', response.data);
      toast.success('Vaccine added successfully');
    })
    .catch(error => {
      console.error('Error adding vaccine:', error);
      toast.error('Failed to add vaccine');
    });
  };
  
  return (
    <div>
      <Navbar />
      <div className='body'>
        <div className="bashcontainer">
          <form onSubmit={handleSubmit}>
            <h2 className="form-title">Add Vaccines</h2>
            <br />
            <div className="form-group">
              <label htmlFor="name">Name</label>
              <input type="text" id="name" name="name" value={formData.name} onChange={handleChange} placeholder="Enter Vaccine Name" required />
            </div>
            <div className="form-group">
              <label htmlFor="Precautions">Precautions</label>
              <input type="text" id="Precautions" name="Precautions" value={formData.Precautions} onChange={handleChange} placeholder="Enter Vaccine Precautions" required />
            </div>
            <div className="form-group">
              <label htmlFor="timeGapFirstSecondDoseInDays">Time Gap First Second Dose In Days</label>
              <input type="text" id="timeGapFirstSecondDoseInDays" name="timeGapFirstSecondDoseInDays" value={formData.timeGapFirstSecondDoseInDays} onChange={handleChange} placeholder="Enter Time Gap First Second Dose In Days" required />
            </div>
            <div>
              <button type="submit" className="btnn">Add</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default AddVaccine;

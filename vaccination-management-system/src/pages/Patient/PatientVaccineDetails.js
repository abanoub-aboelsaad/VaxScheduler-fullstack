import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify'; // Import toast from react-toastify
import 'react-toastify/dist/ReactToastify.css';
import Navbar from '../../components/Navbar';
import { useAuthContext } from '../Auth/hooks/useAuthContext';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function PatientVaccineDetails() {
  const { id } = useParams();
  const { decodeToken, token } = useAuthContext(); 
  const decodedToken = decodeToken(token); 
  const UserId = decodedToken ? decodedToken.nameid : null; 
  const [vaccineDetails, setVaccineDetails] = useState(null);
  const [reservationDate, setReservationDate] = useState(new Date());

  useEffect(() => {
    const fetchVaccineDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:8080/api/vaccines/${id}`);
        setVaccineDetails(response.data);
      } catch (error) {
        console.error('Error fetching vaccine details:', error);
      }
    };

    fetchVaccineDetails();
  }, [id]);

  const handleReservation = async (doseNumber) => {
    try {
      const response = await axios.post(`http://localhost:8080/api/Reservation`, {
        doseNumber: doseNumber,
        reservationDate: reservationDate.toISOString(),
        appUserId: UserId,
        vaccineId: id,
        vaccinationCenterId: vaccineDetails.vaccinationCenterId,
      });
      
    
      // Display success message from response
      toast.success(response.data.message);
      // console.log(response.data)
      console.log(response.data.message)
    } catch (error) {
      console.error('Error reserving dose:', error);
      // Display error message from response
      // console.log(error.response.data)
      toast.error(error.response.data);
    }
  };

  return (
    <div>
      <Navbar />
      <link rel="stylesheet" href="/assets/PatientVaccineDetails/PatientVaccineDetails.css"></link>
      <h1>Vaccine Details</h1>
      {vaccineDetails && (
        <div className="container">
          <div className="product-card">
            <div className="image">
              <img src="/photo.png" alt="" />
            </div>
            <div className="card-content">
              <h3>Name: {vaccineDetails.name}</h3>
              <h3>Precautions: {vaccineDetails.precautions}</h3>
              <h3>Time Needed Between doses: {vaccineDetails.timeGapFirstSecondDoseInDays}</h3>
              <h3>Choose date to get the dose: <DatePicker selected={reservationDate} onChange={date => setReservationDate(date)} /></h3>  
              <div className="store-purchase">
                <div className="buy">
                  <button className="dose" onClick={() => handleReservation('FirstDose')}>Reserve first dose</button>
                  <button className="dose" onClick={() => handleReservation('SecondDose')}>Reserve second dose</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default PatientVaccineDetails;

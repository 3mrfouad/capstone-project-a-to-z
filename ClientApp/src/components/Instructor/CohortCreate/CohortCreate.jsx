import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createCohort } from "../../../actions/instructorActions";

const CohortCreate = ({ history }) => {
  const [name, setName] = useState("");
  const [capacity, setCapacity] = useState("");
  const [modeOfTeaching, setModeOfTeaching] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [city, setCity] = useState("");
  const [validated, setValidated] = useState(false);
  const [invalidDatesBL, setInvalidDatesBl] = useState(false);
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let validStartDate = false;
  let validEndDate = false;
  let formSubmitIndicator = false;

  const cohortCreate = useSelector((state) => state.cohortCreate);
  const { loading, error, cohort, success } = cohortCreate;

  const dispatch = useDispatch();

  // ! (10.2) Anti-tamper validation - Validate (parameters)
  function Validate(name, capacity, city, modeOfTeaching, startDate, endDate) {
    let parsedEndDate = 0;
    let parsedStartDate = 0;
    formSubmitIndicator = true;

    try {
      name = name.trim().toLowerCase();
      capacity = capacity.trim().toLowerCase();
      city = city.trim().toLowerCase();
      modeOfTeaching = modeOfTeaching.trim().toLowerCase();
      startDate = startDate.trim().toLowerCase();
      endDate = endDate.trim().toLowerCase();

      if (!name) {
        validFormData = false;
      } else if (name.Length > 50) {
        validFormData = false;
      } else if (parseInt(capacity) > 999 || parseInt(capacity) < 0) {
        validFormData = false;
      } else if (!city) {
        validFormData = false;
      } else if (city.Length > 50) {
        validFormData = false;
      } else if (
        !(city === "edmonton" || city === "calgary" || city === "other")
      ) {
        validFormData = false;
      } else if (!modeOfTeaching) {
        validFormData = false;
      } else if (modeOfTeaching.Length > 50) {
        validFormData = false;
      } else if (
        !(modeOfTeaching === "online" || modeOfTeaching === "in person")
      ) {
        validFormData = false;
      } else if (!startDate || !endDate) {
        validFormData = false;
      } else {
        try {
          parsedStartDate = Date.parse(startDate);
          validStartDate = true;
        } catch (ParseException) {
          validStartDate = false;
          validFormData = false;
        }
        try {
          parsedEndDate = Date.parse(startDate);
          validEndDate = true;
        } catch (ParseException) {
          validEndDate = false;
          validFormData = false;
        }
        /* Dates business logic */

        if (validStartDate && validEndDate) {
          if (parsedEndDate < parsedStartDate) {
            validFormData = false;
          } else {
            validFormData = true;
          }
        }
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
  // ! ------------------------------------------------------

  const submitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    setValidated(true);

    //(3) Add business logic
    if (
      endDate === "" ||
      startDate === "" ||
      Date.parse(endDate) < Date.parse(startDate)
    ) {
      e.preventDefault();
      Date.parse(endDate) < Date.parse(startDate)
        ? setInvalidDatesBl(true)
        : setInvalidDatesBl(false);
      setEndDate("");

      // ! (10.3) Anti-tamper validation - Alert message conditions
      validFormData = false;
      formSubmitIndicator = true;
      setValidData(validFormData);
      // ! ------------------------------------------------------
    } else {
      e.preventDefault();
      setInvalidDatesBl(false);
      //----------------------------

      // ! (10.4) Anti-tamper validation - calling Validate
      Validate(name, capacity, city, modeOfTeaching, startDate, endDate);
      if (validFormData) {
        setValidData(validFormData);
        // ! ------------------------------------------------------
        e.preventDefault();
        dispatch(
          createCohort({
            name,
            capacity,
            city,
            modeOfTeaching,
            startDate,
            endDate,
          })
        );
      } else {
        // ! (10.5) Anti-tamper validation - Alert message conditions
        setValidData(validFormData);
      }
    }
    // ! (10.6) Anti-tamper validation - Alert message conditions
    setFormSubmitted(formSubmitIndicator);
    // ! ------------------------------------------------------
  };
  const goBack = () => {
    history.goBack();
  };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h2>Cohort</h2>

            {/* ! (10.7) Anti-tamper validation - Alert message conditions   */}
            <p
              class={
                formSubmitted
                  ? validData
                    ? !loading && error
                      ? "alert alert-danger"
                      : !loading && !error && success
                      ? "alert alert-success"
                      : ""
                    : "alert alert-danger"
                  : ""
              }
              role="alert"
            >
              {formSubmitted
                ? validData
                  ? !loading && error
                    ? `Unsuccessful attempt to create a cohort.\r\n${error.data}`
                    : !loading && !error && success
                    ? "Cohort was successfully created"
                    : ""
                  : "Error: Form was submitted with invalid data fields"
                : ""}
            </p>
            <Form noValidate validated={validated} onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  required
                  type="text"
                  maxLength="50"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please enter a cohort name.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="Capacity">
                <Form.Label>Capacity</Form.Label>
                <Form.Control
                  type="number"
                  min={0}
                  max={999}
                  step="1"
                  value={capacity}
                  onChange={(e) => setCapacity(String(e.target.value))}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="Mode of Teaching">
                <Form.Label>Mode of Teaching</Form.Label>
                <Form.Control
                  as="select"
                  required
                  value={modeOfTeaching}
                  onChange={(e) => setModeOfTeaching(String(e.target.value))}
                >
                  <option value="">Select</option>
                  <option>Online</option>
                  <option>In Person</option>
                </Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose a mode of teaching.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="Start Date">
                <Form.Label>Start Date</Form.Label>
                <Form.Control
                  required
                  type="date"
                  value={startDate}
                  onChange={(e) => setStartDate(String(e.target.value))}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose a start date.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="End Date">
                <Form.Label>End Date</Form.Label>
                <Form.Control
                  required
                  type="date"
                  min={startDate}
                  value={endDate}
                  onChange={(e) => setEndDate(String(e.target.value))}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose an end date.
                </Form.Control.Feedback>
                <p className="text-danger small">
                  {invalidDatesBL ? "End date can't be before start date" : ""}
                </p>
              </Form.Group>
              <Form.Group controlId="City">
                <Form.Label>City</Form.Label>
                <Form.Control
                  as="select"
                  required
                  value={city}
                  onChange={(e) => setCity(e.target.value)}
                >
                  <option value="">Select</option>
                  <option value="Edmonton">Edmonton</option>
                  <option value="Calgary">Calgary</option>
                  <option value="Other">Other</option>
                </Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose a city.
                </Form.Control.Feedback>
              </Form.Group>
              <button type="button" className="btn btn-link" onClick={goBack}>
                Back
              </button>{" "}
              <Button className="float-right" type="submit" variant="primary">
                {" "}
                Save
              </Button>
            </Form>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default CohortCreate;

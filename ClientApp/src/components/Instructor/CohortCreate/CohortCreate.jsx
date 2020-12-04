import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createCohort } from "../../../actions/instructorActions";

const CohortCreate = () => {
  const [name, setName] = useState("");
  const [capacity, setCapacity] = useState("");
  const [modeOfTeaching, setModeOfTeaching] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [city, setCity] = useState("");

  //(1) Add validation states and variables
  const [validated, setValidated] = useState(false);
  const [invalidDatesBL, setInvalidDatesBl] = useState(false);
  // ------------------------------------------------------

  // ! (10.1) Anti-tamper validation - States and Variables
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let validStartDate = false;
  let validEndDate = false;
  let formSubmitIndicator = false;
  // ! ------------------------------------------------------

  useEffect(() => {
    // get cohort by id
    // populate the cohort data in here
  }, []);
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
        console.log("capacity: ", parseInt(capacity));
      } else if (!city) {
        validFormData = false;
        console.log("city");
      } else if (city.Length > 50) {
        validFormData = false;
        console.log("city length");
      } else if (
        !(city === "edmonton" || city === "calgary" || city === "other")
      ) {
        validFormData = false;
        console.log(
          "modeOfTeaching value:",
          modeOfTeaching.toLowerCase(),
          "original:",
          modeOfTeaching
        );
      } else if (!modeOfTeaching) {
        validFormData = false;
        console.log("modeOfTeaching");
      } else if (modeOfTeaching.Length > 50) {
        validFormData = false;
        console.log("modeOfTeaching length");
      } else if (
        !(modeOfTeaching === "online" || modeOfTeaching === "in person")
      ) {
        validFormData = false;
        console.log(
          "modeOfTeaching value:",
          modeOfTeaching.toLowerCase(),
          "original:",
          modeOfTeaching
        );
      } else if (!startDate || !endDate) {
        validFormData = false;
        console.log("startDate/endDate");
      } else {
        try {
          parsedStartDate = Date.parse(startDate);
          validStartDate = true;
          console.log("startDate parse");
        } catch (ParseException) {
          validStartDate = false;
          console.log("startDate parse exception");
          validFormData = false;
        }
        try {
          parsedEndDate = Date.parse(startDate);
          validEndDate = true;
          console.log("endDate parse");
        } catch (ParseException) {
          validEndDate = false;
          console.log("endDate parse exception");
          validFormData = false;
        }
        /* Dates business logic */

        console.log(
          "parsed start date validation: ",
          validStartDate,
          "parsed end date validation: ",
          validEndDate
        );
        if (validStartDate && validEndDate) {
          console.log("Dates are both pursed ok");
          if (parsedEndDate < parsedStartDate) {
            validFormData = false;
            console.log("parsedEndDate < parsedStartDate");
          } else {
            validFormData = true;
            console.log("All good :", validFormData);
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
    console.log("pass initial validation 100");
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
      console.log("pass initial validation 100", validFormData);

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
        console.log("create cohort");
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
                    ? "Unsuccessful attempt to create a cohort"
                    : !loading && !error && success
                    ? "Cohort was successfully created"
                    : ""
                  : "Error: Form were submitted with invalid data fields"
                : ""}
            </p>
            {/* ! ------------------------------------------------------  */}
            <Form noValidate validated={validated} onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Name</Form.Label>
                {/*
                 * (4) Add required to all required input fields
                 * (5) For type = "text", use maxlength ={50} i.e., according to the actual length from ERD
                 * (5) If type is not "text", add String(e.target.value) to the onchange
                 * (6) If type is "number", add min={0}, and max={999} i.e., according to actual range from ERD
                 * (7) For dropdown menu, use as="select", and add one empty option above other options <option></option>
                 */}
                <Form.Control
                  required
                  type="text"
                  maxLength="50"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                ></Form.Control>
                {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please enter a cohort name.
                </Form.Control.Feedback>

    };
    return (
        <React.Fragment>
            <Container>
                <Row className="justify-content-md-center">
                    <Col xs={12} md={6}>
                        <h2>Cohort</h2>
                        {/* ! (10.7) Anti-tamper validation - Alert message conditions   */}
                        <p className=
                            {
                                formSubmitted ? (validData ? ((!loading && error) ? "alert alert-danger" :
                                    ((!loading && !error && success) ? "alert alert-success" : "")) :
                                    "alert alert-danger") : ""
                            }
                            role="alert">
                            {
                                formSubmitted ? (validData ? ((!loading && error) ? "Unsuccessful attempt to create a cohort" :
                                    ((!loading && !error && success) ? "Cohort was successfully created" : "")) :
                                    "Error: Form were submitted with invalid data fields") : ""
                            }
                        </p>
                        {/* ! ------------------------------------------------------  */}
                        <Form noValidate validated={validated} onSubmit={submitHandler}>
                            <Form.Group controlId="name">
                                <Form.Label>Name</Form.Label>
                                {
                                    /*
                                     * (4) Add required to all required input fields
                                     * (5) For type = "text", use maxlength ={50} i.e., according to the actual length from ERD
                                     * (5) If type is not "text", add String(e.target.value) to the onchange
                                     * (6) If type is "number", add min={0}, and max={999} i.e., according to actual range from ERD
                                     * (7) For dropdown menu, use as="select", and add one empty option above other options <option></option>
                                     */
                                }
                                <Form.Control
                                    required
                                    type="text"
                                    maxLength="50"
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                ></Form.Control>
                                {
                                    /*(8) Add Form control feedback.*/
                                }
                                <Form.Control.Feedback type="invalid">
                                    Please enter a cohort name.
                                </Form.Control.Feedback>

                                {/*---------------------------------------*/}
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
                                ><option value="">Select</option>
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
                                {/* (9) Add business logic validation message. */}
                                <p className="text-danger small">{invalidDatesBL ? "End date can't be before start date" : ""}</p>
                                {/*---------------------------------------*/}
                            </Form.Group>
                            <Form.Group controlId="City">
                                <Form.Label>City</Form.Label>
                                <Form.Control as="select"
                                    required
                                    value={city}
                                    onChange={(e) => setCity(e.target.value)}
                                >
                                    <option value="">Select</option>
                                    <option>Edmonton</option>
                                    <option>Calgary</option>
                                    <option>Other</option>
                                </Form.Control>
                                <Form.Control.Feedback type="invalid">
                                    Please choose a city.
                                </Form.Control.Feedback>
                            </Form.Group>
                            <a href="">Back</a>
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

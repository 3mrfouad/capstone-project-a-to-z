import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { editCohort, cohortGet } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const CohortEdit = ({ match, history }) => {
  const cohortId = match.params.id;
  console.log("cohort edit", cohortId);
  const [name, setName] = useState("");
  const [capacity, setCapacity] = useState("");
  const [modeOfTeaching, setModeOfTeaching] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [city, setCity] = useState("");
  const [validated, setValidated] = useState(false);
  const [invalidDatesBL, setInvalidDatesBl] = useState(false);

  // ! (10.1) Anti-tamper validation - States and Variables
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let validStartDate = false;
  let validEndDate = false;
  let formSubmitIndicator = false;

  const dispatch = useDispatch();
  const cohortEdit = useSelector((state) => state.cohortEdit);
  const cohortGetState = useSelector((state) => state.cohortGetState);
  const { error, success } = cohortEdit;
  const { cohort, loading } = cohortGetState;

  useEffect(() => {
    if (!cohort || !cohort.name || cohort.cohortId != cohortId) {
      dispatch(cohortGet(cohortId));
    } else {
      setName(cohort.name);
      setCapacity(cohort.capacity);
      setCity(cohort.city);
      setEndDate(cohort.endDate);
      setStartDate(cohort.startDate);
      setModeOfTeaching(cohort.modeOfTeaching);
    }
  }, [dispatch, cohort]);

  // ! (10.2) Anti-tamper validation - Validate (parameters)
  function Validate(
    cohortId,
    name,
    capacity,
    city,
    modeOfTeaching,
    startDate,
    endDate
  ) {
    let parsedEndDate = 0;
    let parsedStartDate = 0;
    formSubmitIndicator = true;

    try {
      console.log("try");
      cohortId = cohortId.trim().toLowerCase();
      console.log("cohortID: ", cohortId);

      name = name.trim().toLowerCase();
      console.log("name trim : ", name);

      capacity = capacity.trim().toLowerCase();
      console.log("cap trim");

      city = city.trim().toLowerCase();
      console.log("city trim");

      modeOfTeaching = modeOfTeaching.trim().toLowerCase();
      console.log("mode trim");

      startDate = startDate.trim().toLowerCase();
      console.log("start trim");

      endDate = endDate.trim().toLowerCase();
      console.log("enddate trim");

      if (!cohortId) {
        validFormData = false;
        console.log("no cohortId");
      } else if (cohortId < 0 || cohortId > 2147483647) {
        validFormData = false;
        console.log("out of range cohort Id");
      } else if (!name) {
        validFormData = false;
        console.log("name validate");
      } else if (name.Length > 50) {
        validFormData = false;
        console.log("namelength");
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
        console.log("city value:", city.toLowerCase(), "original:", city);
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
          console.log("endDate purse");
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

  const goBack = () => {
    history.goBack();
  };

  const submitHandler = (e) => {
    e.preventDefault();
    // this is where we handle the user inputs
    // this is where we need validation
    // the dispatch call will be conditioned  if else statement or equivalent

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
      console.log("pass initial validation 100", validFormData);
      // ! (10.3) Anti-tamper validation - Alert message conditions
      validFormData = false;
      formSubmitIndicator = true;
      setValidData(validFormData);
      // ! ------------------------------------------------------
    } else {
      e.preventDefault();
      setInvalidDatesBl(false);
      // ! (10.4) Anti-tamper validation - calling Validate
      Validate(
        cohortId,
        name,
        String(capacity),
        city,
        modeOfTeaching,
        startDate,
        endDate
      );
      if (validFormData) {
        setValidData(validFormData);
        // ! ------------------------------------------------------
        console.log("update cohort");
        console.log(startDate);
        dispatch(
          editCohort({
            cohortId,
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
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            {/* check the pagination */}
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
                      ? "Cohort details were successfully updated"
                      : ""
                    : "Error: Form was submitted with invalid data fields"
                  : ""}
              </p>
              {/* ! ------------------------------------------------------  */}
              <Form noValidate validated={validated} onSubmit={submitHandler}>
                <Form.Group controlId="name">
                  <Form.Label>Cohort Name</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    maxlength="50"
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
                    value={capacity ? capacity : 0}
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
                    <option></option>
                    <option>Online</option>
                    <option>In Person</option>
                  </Form.Control>
                </Form.Group>
                <Form.Group controlId="Start Date">
                  <Form.Label>Start Date</Form.Label>
                  <Form.Control
                    required
                    type="date"
                    value={startDate.split("T")[0]}
                    onChange={(e) =>
                      setStartDate(String(e.target.value).split("T")[0])
                    }
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
                    value={endDate.split("T")[0]}
                    onChange={(e) =>
                      setEndDate(String(e.target.value).split("T")[0])
                    }
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please choose an end date.
                  </Form.Control.Feedback>
                  <p className="text-danger small">
                    {invalidDatesBL
                      ? "End date can't be before start date"
                      : ""}
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
                    <option></option>
                    <option>Edmonton</option>
                    <option>Calgary</option>
                    <option>Other</option>
                  </Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please choose a city.
                  </Form.Control.Feedback>
                </Form.Group>
                <button type="button" className="btn btn-link" onClick={goBack}>
                  Back
                </button>{" "}
                <Button type="submit" variant="primary" className="float-right">
                  {" "}
                  Save
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};
export default CohortEdit;

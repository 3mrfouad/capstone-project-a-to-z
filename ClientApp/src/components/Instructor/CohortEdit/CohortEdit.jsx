import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { editCohort, cohortGet } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const CohortEdit = ({ match, history }) => {
  const cohortId = match.params.id;
  console.log(cohortId);
  const [name, setName] = useState("");
  const [capacity, setCapacity] = useState("");
  const [modeOfTeaching, setModeOfTeaching] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [city, setCity] = useState("");
  const [validated, setValidated] = useState(false);
  const [invalidDatesBL, setInvalidDatesBl] = useState(false);
  const dispatch = useDispatch();

  const cohortEdit = useSelector((state) => state.cohortEdit);
  const cohortGetState = useSelector((state) => state.cohortGetState);
  const { error, success } = cohortEdit;
  const { cohort, loading } = cohortGetState;

  useEffect(() => {
      if (!cohort || !cohort.name) {
        dispatch(cohortGet(cohortId));
      } else {
        setName(cohort.name);
        setCapacity(cohort.capacity);
        setCity(cohort.city);
        setEndDate(cohort.endDate);
        setStartDate(cohort.startDate);
        setModeOfTeaching(cohort.modeOfTeaching);
      }
    },
    [dispatch, cohort]);

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
    if (endDate === "" ||
        startDate === "" ||
        Date.parse(endDate) < Date.parse(startDate)) {
        e.preventDefault();
        Date.parse(endDate) < Date.parse(startDate)
            ? setInvalidDatesBl(true)
            : setInvalidDatesBl(false);
        setEndDate("");
    }
    else
    {

        setInvalidDatesBl(false);
        e.preventDefault();
        console.log("update cohort");
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
    }
  };
    return (
      <React.Fragment>
        {loading
          ? (<Loader/>)
          : (<Container>
               <Row className="justify-content-md-center">
                 { /* check the pagination */
                 }
                 <Col xs={12} md={6}>
                   <h2>Cohort</h2>
                   <Form noValidate validated={validated} onSubmit={
submitHandler}>
                     <Form.Group controlId="name">
                       <Form.Label>Cohort Name</Form.Label>
                       <Form.Control
                         required
                                        type="text"
                         maxlength="50"
                         value={name}
                         onChange={(e) => setName(e.target.value)
}>
                       </Form.Control>
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
                         onChange={(e) => setCapacity(String(e.target.value))
}>
                       </Form.Control>
                     </Form.Group>
                     <Form.Group controlId="Mode of Teaching">
                       <Form.Label>Mode of Teaching</Form.Label>
                       <Form.Control
                         as="select"
                         required
                         value={modeOfTeaching}
                         onChange={(e) => setModeOfTeaching(
                           String(e.target.value))}>
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
                         value={startDate}
                         onChange={(e) => setStartDate(String(e.target.value))
}>
                       </Form.Control>
                       <Form.Control.Feedback type="invalid">
                         Please choose a start date.
                       </Form.Control.Feedback>
                     </Form.Group>
                     <Form.Group controlId="End Date">
                       <Form.Label>End Date</Form.Label>
                       <Form.Control
                         required
                         type="date"
                         value={endDate}
                         onChange={(e) => setEndDate(String(e.target.value))
}>
                       </Form.Control>
                       <Form.Control.Feedback type="invalid">
                         Please choose an end date.
                       </Form.Control.Feedback>
                       <p className="text-danger small">{invalidDatesBL
                         ? "End date can't be before start date"
                         : ""}</p>
                     </Form.Group>
                     <Form.Group controlId="City">
                       <Form.Label>City</Form.Label>
                       <Form.Control
                         as="select"
                         required
                         value={city}
                         onChange={(e) => setCity(e.target.value)}>
                         <option></option>
                         <option>Edmonton</option>
                         <option>Calgary</option>
                         <option>Other</option>
                       </Form.Control>
                       <Form.Control.Feedback type="invalid">
                         Please choose a city.
                       </Form.Control.Feedback>
                     </Form.Group>
                     <a href="">Back</a>
                     <Button type="submit" variant="primary" className="float-right">
                       {" "}
                       Save
                     </Button>
                   </Form>
                 </Col>
               </Row>
             </Container>)}
      </React.Fragment>
    );
  };
  export default
  CohortEdit;;
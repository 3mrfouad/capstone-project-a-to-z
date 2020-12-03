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

    //(1) Add validation states
    const [validated, setValidated] = useState(false);
    const [invalidDatesBL, setInvalidDatesBl] = useState(false);
    const [submitFeedbackAlert, setSubmitFeedbackAlert] = useState("");
    const [validData, setValidData] = useState(true);
    const [validStartDate,setValidStartDate]=useState(false);
    const [validEndDate,setValidEndDate]=useState(false);
    let temp = false;
    //----------------------------

    useEffect(() => {
        // get cohort by id
        // populate the cohort data in here
    }, []);
    const cohortCreate = useSelector((state) => state.cohortCreate);
    const { loading, error, cohort, success } = cohortCreate;
    const dispatch = useDispatch();

    function Validate (name,
        capacity,
        city,
        modeOfTeaching,
        startDate,
        endDate) {
       
        /* Name validation */
        name = (!name.trim())? null:name.trim().toLowerCase();
        (!name)? setValidData(false):setValidData(true);
        (name.Length > 50)? setValidData(false):setValidData(true);
    
        /* Capacity validation */
        capacity = (!capacity.trim())? null: capacity.trim().toLowerCase();
        if (!capacity){
        (parseInt(capacity) > 999 || parseInt(capacity) < 0)? setValidData(false):setValidData(true); }
       
        /* City validation */
        city = (!city.trim())? null:city.trim().toLowerCase();
        (!city)? setValidData(false):setValidData(true);
        (city.Length > 50)? setValidData(false):setValidData(true);
    
        /* ModeOfTeaching validation */
       modeOfTeaching = (!modeOfTeaching.trim())? null: modeOfTeaching.trim().toLowerCase();
       (!modeOfTeaching)? setValidData(false):setValidData(true);
       (modeOfTeaching.Length > 50)? setValidData(false):setValidData(true);
       (modeOfTeaching.toLowerCase() === "online"|| modeOfTeaching.toLowerCase() === "in-person")? setValidData(false):setValidData(true);
    
        /* Start date validation */
       startDate = (!startDate.trim())? null: modeOfTeaching.trim().toLowerCase();
      if (!startDate) setValidData(false)
      else
      {
        try
        {
            const parsedDate = Date.parse(startDate);
            setValidStartDate(true) ;
        }
        catch (ParseException)
        {
            setValidStartDate(false);
            setValidData(false) ;
        }
      }
      
      /* End date validation */
      endDate = (!endDate.trim())? null: endDate.trim().toLowerCase();
      if (!endDate) setValidData(false)
      else
      {
        try
        {
            const parsedDate = Date.parse(startDate); 
            setValidEndDate(true) ;
        }
        catch (ParseException)
        {
            setValidEndDate(false);        
            setValidData(false) ;
        }
      }
        
        /* Dates business logic */
            if (validStartDate && validEndDate)
            {
                (endDate < startDate)? setValidData(false):setValidData(true); 
            }
 };

    const submitHandler = (e) => {

        //(2) Add form validation condition block if-else
        const form = e.currentTarget;
        if (form.checkValidity() === false) 
        {
            e.preventDefault();
            e.stopPropagation();
        }
        
        setValidated(true);

        //(3) Add business logic
        if (endDate === "" || startDate==="" || Date.parse(endDate) < Date.parse(startDate)) 
        {
            e.preventDefault();
            Date.parse(endDate) < Date.parse(startDate) ? setInvalidDatesBl(true) : setInvalidDatesBl(false);
            setEndDate("");
        }
        else 
        {
            e.preventDefault();
            setInvalidDatesBl(false);
            //----------------------------

            /*
             ---------------------------------------------------------------------------------
             Post HTML5 regular validation (Frontend validation / BL)*/
             Validate (name,
                capacity,
                city,
                modeOfTeaching,
                startDate,
                endDate);
             if (validData)
            {
                   setSubmitFeedbackAlert("Error: Form were submitted with invalid data fields")
            }
             else
            {
/*----------------------------------------------------------------------------------
                    */

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
/*----------------------------------------------------------------------------------
                    */
                 if (!loading) error? setSubmitFeedbackAlert("Unsuccessful attempt to create a cohort"):setSubmitFeedbackAlert("Cohort was successfully created");
            }
        }
    };
    return (
        <React.Fragment>
            <Container>
                <Row className="justify-content-md-center">
                    <Col xs={12} md={6}>
                        <h2>Cohort</h2>
                        <div class= {loading? (error? "alert alert-danger":"alert alert-success"):""} role="alert">
                        {submitFeedbackAlert}
                        </div>
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
                                    maxlength ="50"
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
                                ><option></option>
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

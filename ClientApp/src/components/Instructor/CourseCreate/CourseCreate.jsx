import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createCourse } from "../../../actions/instructorActions";

const CourseCreate = () => {
  const [courseName, setCourseName] = useState("");
  const [hours, setHours] = useState("");
  const [description, setDescription] = useState("");
  const dispatch = useDispatch();

   //(1) Add validation states
   const [validated, setValidated] = useState(false);   
   //----------------------------

  useEffect(() => {
    // get course by id
    // populate the cohort data in here
  }, []);
  const courseCreate = useSelector((state) => state.courseCreate);
  const { loading, error, course } = courseCreate;
  const submitHandler = (e) => {

    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
        e.preventDefault();
        e.stopPropagation();
    }
    setValidated(true);      
  //(3) Add business logic- No business Logic for now
    e.preventDefault();
    console.log("create course");
    dispatch(
      createCourse({
        courseName,
        hours,
        description,
      })
    );
  };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h2>Course</h2>
            <Form onSubmit={submitHandler}>
              <Form.Group controlId="CourseName">
                <Form.Label>Course Name</Form.Label>

                <Form.Control
                  required
                  type="text"
                  maxlength ="50"
                  value={courseName}
                  onChange={(e) => setCourseName(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                    Please enter a course name.
                </Form.Control.Feedback>

                <Form.Label className="mr-5">Hours</Form.Label>

                <Form.Control
                required
                 type="number"
                 min={0}
                 max={999.99}
                 step="0.1"
                  value={hours}
                  onChange={(e) => setHours(String(e.target.value))}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                    Please fill in the Hours field.
                </Form.Control.Feedback>
                <Form.Label>Description</Form.Label>

                <Form.Control
                  required
                  type="text"
                  max={250}
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                    Please enter description for Course.
                </Form.Control.Feedback>

              </Form.Group>
              <button type="button" className="btn btn-link">
                Back
              </button>{" "}
              <Button type="submit" className="float-right">
                Create Course
              </Button>
            </Form>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default CourseCreate;

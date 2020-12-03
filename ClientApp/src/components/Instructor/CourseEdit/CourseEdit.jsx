import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { editCourse, getCourse } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";
import Message from "../../shared/Message/Message";

const CourseEdit = ({ match, history }) => {
  const courseId = match.params.id;
  const [courseName, setCourseName] = useState("");
  const [hours, setHours] = useState("");
  const [description, setDescription] = useState("");
  const dispatch = useDispatch();
//(1) Add validation states
const [validated, setValidated] = useState(false);   
//----------------------------

  const courseEdit = useSelector((state) => state.courseEdit);
  const getCourseDetail = useSelector((state) => state.getCourse);
  const { success } = courseEdit;
  const { loading, error, course } = getCourseDetail;

  useEffect(() => {
    if (success) {
      dispatch({
        type: "COURSE_EDIT_RESET",
      });
    } else {
      if (!course || !course.name) {
        dispatch(getCourse(courseId));
      } else {
        setCourseName(course.name);
        setDescription(course.description);
        setHours(course.durationHrs);
      }
    }
  }, [dispatch, course, success]);

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
    console.log("edit course");
    dispatch(
      editCourse({
        courseId,
        name: courseName,
        description,
        durationHrs: hours,
      })
    );
  };
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h2>Course</h2>
              {success && (
                <Message variant="success">Update Successfully</Message>
              )}
              <Form noValidate validated={validated} onSubmit={submitHandler}>
                <Form.Group controlId="">
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
                    onChange={(e) => setHours(e.target.value)}
                  ></Form.Control>

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

export default CourseEdit;

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
              <Form onSubmit={submitHandler}>
                <Form.Group controlId="">
                  <Form.Label>Course Name</Form.Label>

                  <Form.Control
                    type="text"
                    value={courseName}
                    onChange={(e) => setCourseName(e.target.value)}
                  ></Form.Control>

                  <Form.Label className="mr-5">Hours</Form.Label>

                  <Form.Control
                    type="text"
                    value={hours}
                    onChange={(e) => setHours(e.target.value)}
                  ></Form.Control>

                  <Form.Label>Description</Form.Label>

                  <Form.Control
                    type="text"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                  ></Form.Control>
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

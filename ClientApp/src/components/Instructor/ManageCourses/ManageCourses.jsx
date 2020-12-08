import React from "react";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Table, Container, Button, Modal, ButtonGroup } from "react-bootstrap";
import {
  manageCourseInstructor,
  archiveCourse,
} from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";
import { Link } from "react-router-dom";

const ManageCourseInstructor = ({ history }) => {
  const dispatch = useDispatch();
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const onArchive = (id) => {
    setShow(false);
    dispatch(archiveCourse(id));
  };
  const { courses, loading } = useSelector((state) => state.manageCourse);
  const { success } = useSelector((state) => state.courseArchive);
  const goBack = () => {
    history.goBack();
  };
  useEffect(() => {
    dispatch(manageCourseInstructor());
  }, [dispatch, success]);
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Table>
            <thead>
              <tr>
                <th>Course Name</th>
                <th>Description</th>
                <th>Duration</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {courses
                .filter((course) => course.archive == false)
                .map((course, index) => (
                  <tr key={index}>
                    <td>{course.name}</td>
                    <td>{course.description}</td>
                    <td>{course.durationHrs}</td>
                    <td>
                      <ButtonGroup className="float-left">
                        <Button
                          variant="link"
                          href={`/courseedit/${course.courseId}`}
                          className="float-left"
                        >
                          Edit
                        </Button>{" "}
                        <Button variant="link" onClick={handleShow}>
                          Archive
                        </Button>
                      </ButtonGroup>

                      <Modal show={show} onHide={handleClose}>
                        <Modal.Body>Archive: Are you sure?</Modal.Body>
                        <Modal.Footer>
                          <Button variant="secondary" onClick={handleClose}>
                            No
                          </Button>
                          <Button
                            variant="primary"
                            onClick={() => onArchive(course.courseId)}
                          >
                            Yes
                          </Button>
                        </Modal.Footer>
                      </Modal>
                    </td>
                  </tr>
                ))}
            </tbody>
          </Table>
          <button type="button" className="btn btn-link" onClick={goBack}>
            Back
          </button>{" "}
          <Link to="/coursecreate">
            <Button className="float-right mr-3">Create Course</Button>{" "}
          </Link>
        </Container>
      )}
    </React.Fragment>
  );
};

export default ManageCourseInstructor;

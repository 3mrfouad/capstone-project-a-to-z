import React from "react";
import { Navbar, Nav, Container } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import Loader from "../Loader/Loader";
import { logout } from "../../../actions/instructorActions";
import styles from "./Header.module.css";

const Header = () => {
  const dispatch = useDispatch();
  const { user, success, loading } = useSelector(
    (state) => state.userLoginState
  );

  const logoutHandler = () => {
    dispatch(logout());
  };
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Navbar bg="primary" expand="lg" collapseOnSelect className="mb-5">
          <Container>
            <LinkContainer to="/">
              <Navbar.Brand>AZ Learn</Navbar.Brand>
            </LinkContainer>
            {user && user.isInstructor ? (
              <React.Fragment>
                <Nav>
                  <LinkContainer to={"/cohortsummary"}>
                    <Nav.Link>
                      {" "}
                      <span className={styles.fontNavLink}>Cohorts</span>{" "}
                    </Nav.Link>
                  </LinkContainer>
                  <LinkContainer to={`/registeruser`}>
                    <Nav.Link>
                      {" "}
                      <span className={styles.fontNavLink}>
                        Register Users
                      </span>{" "}
                    </Nav.Link>
                  </LinkContainer>
                  <LinkContainer to={`/managecourse`}>
                    <Nav.Link>
                      {" "}
                      <span className={styles.fontNavLink}>
                        Manage Course
                      </span>{" "}
                    </Nav.Link>
                  </LinkContainer>
                </Nav>
              </React.Fragment>
            ) : (
              ""
            )}
            {user && !user.isInstructor ? (
              <React.Fragment>
                <Nav>
                  <LinkContainer to={`/student/${user.userId}`}>
                    <Nav.Link>
                      {" "}
                      <span className={styles.fontNavLink}>Courses</span>{" "}
                    </Nav.Link>
                  </LinkContainer>
                </Nav>
              </React.Fragment>
            ) : (
              ""
            )}
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
              <Nav className="ml-auto">
                <LinkContainer to="/">
                  <Nav.Link>
                    {" "}
                    <i className="fas fa-shopping-cart"></i>{" "}
                    <strong>Hello {user ? user.name : ""}</strong>{" "}
                  </Nav.Link>
                </LinkContainer>
                {user ? (
                  <LinkContainer to="/" onClick={logoutHandler}>
                    <Nav.Link>
                      {" "}
                      <strong>LogOut</strong>{" "}
                    </Nav.Link>
                  </LinkContainer>
                ) : (
                  ""
                )}
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      )}
    </React.Fragment>
  );
};

export default Header;

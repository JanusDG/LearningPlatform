using LearningPlatform.Models;

namespace LearningPlatform.Helpers;
public static class ViewEntityMapper {
    
    public static CourseViewModel GetCourseViewModel(CourseEntityModel course)
    {
        return new CourseViewModel
        {
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons,
        };
    }

    public static List<CourseViewModel> GetCourseViewModels(List<CourseEntityModel> courses)
    {
        return courses.Select(courseEM => 
            new CourseViewModel
                {
                    UserCourses = courseEM.UserCourses,
                    Name = courseEM.Name,
                    Description = courseEM.Description,
                    Lessons = courseEM.Lessons,
                }
            ).ToList();
    }

    public static CourseViewIdModel GetCourseViewIdModel(CourseEntityModel course)
    {
        return new CourseViewIdModel
        {
            Id = course.Id,
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons,
        };
    }

    public static List<CourseViewIdModel> GetCourseViewIdModels(List<CourseEntityModel> courses)
    {
        return courses.Select(courseEM => 
            new CourseViewIdModel
                {
                    Id = courseEM.Id,
                    UserCourses = courseEM.UserCourses,
                    Name = courseEM.Name,
                    Description = courseEM.Description,
                    Lessons = courseEM.Lessons,
                }
            ).ToList();
    }

    public static CourseEntityModel GetCourseEntityModel(CourseViewModel course)
    {
        return new CourseEntityModel
        {
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons,
        };
    }

    public static List<CourseEntityModel> GetCourseEntityModels(List<CourseViewModel> courses)
    {
        return courses.Select(courseEM => 
            new CourseEntityModel
                {
                    UserCourses = courseEM.UserCourses,
                    Name = courseEM.Name,
                    Description = courseEM.Description,
                    Lessons = courseEM.Lessons,
                }
            ).ToList();
    }

    public static UserViewModel GetUserViewModel(UserEntityModel user)
    {
        return new UserViewModel
        {
            UserCourses = user.UserCourses,
            Username = user.Username,
            Firstname = user.Firstname,
            Surname = user.Surname,
            Email = user.Email,
        };
    }

    public static List<UserViewModel> GetUserViewModels(List<UserEntityModel> users)
    {
        return users.Select(userEM => 
            new UserViewModel
                {
                    UserCourses = userEM.UserCourses,
                    Username = userEM.Username,
                    Firstname = userEM.Firstname,
                    Surname = userEM.Surname,
                    Email = userEM.Email,
                }
            ).ToList();
    }

    public static UserViewIdModel GetUserViewIdModel(UserEntityModel user)
    {
        return new UserViewIdModel
        {
            Id = user.Id,
            UserCourses = user.UserCourses,
            Username = user.Username,
            Firstname = user.Firstname,
            Surname = user.Surname,
            Email = user.Email,
        };
    }

    public static List<UserViewIdModel> GetUserViewIdModels(List<UserEntityModel> users)
    {
        return users.Select(userEM => 
            new UserViewIdModel
                {
                    Id = userEM.Id,
                    UserCourses = userEM.UserCourses,
                    Username = userEM.Username,
                    Firstname = userEM.Firstname,
                    Surname = userEM.Surname,
                    Email = userEM.Email,
                }
            ).ToList();
    }

        public static UserEntityModel GetUserEntityModel(UserViewModel user)
    {
        return new UserEntityModel
        {
            UserCourses = user.UserCourses,
            Username = user.Username,
            Firstname = user.Firstname,
            Surname = user.Surname,
            Email = user.Email,
            
        };
    }

    public static List<UserEntityModel> GetUserEntityModels(List<UserViewModel> users)
    {
        return users.Select(userEM => 
            new UserEntityModel
                {
                    UserCourses = userEM.UserCourses,
                    Username = userEM.Username,
                    Firstname = userEM.Firstname,
                    Surname = userEM.Surname,
                    Email = userEM.Email,
                    
                }
            ).ToList();
    }

}
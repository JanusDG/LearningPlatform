using LearningPlatform.Models;

namespace LearningPlatform.Helpers;
public static class ViewEntityMapper {
    
    public static LessonViewModel? GetLessonViewModel(LessonEntityModel? lesson)
    {
        return new LessonViewModel
        {
            Name = lesson.Name,
            Description = lesson.Description,
        };
    }
    public static List<LessonViewModel?> GetLessonViewModels(List<LessonEntityModel?> lessons)
    {
        return lessons.Select(lessonEM => 
            GetLessonViewModel(lessonEM)
            ).ToList();
    }
    public static LessonViewIdModel? GetLessonViewIdModel(LessonEntityModel? lesson)
    {
        return new LessonViewIdModel
        {
            Id = lesson.Id,
            Name = lesson.Name,
            Description = lesson.Description,
        };
    }
    public static List<LessonViewIdModel?> GetLessonViewIdModels(List<LessonEntityModel?> lessons)
    {
        return lessons.Select(lessonEM => 
            GetLessonViewIdModel(lessonEM)
            ).ToList();
    }
    public static LessonEntityModel? GetLessonEntityModel(LessonViewModel? lesson)
    {
        return new LessonEntityModel
        {
            Name = lesson.Name,
            Description = lesson.Description,
        };
    }
    public static List<LessonEntityModel?> GetLessonEntityModels(List<LessonViewModel?> lessons)
    {
        return lessons.Select(lessonEM => 
            GetLessonEntityModel(lessonEM)
            ).ToList();
    }

    public static CourseViewModel GetCourseViewModel(CourseEntityModel course)
    {
        return new CourseViewModel
        {
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons?.Select(lesson => GetLessonViewModel(lesson)).ToList()
        };
    }

    public static List<CourseViewModel> GetCourseViewModels(List<CourseEntityModel> courses)
    {
        return courses.Select(courseEM => 
            GetCourseViewModel(courseEM)
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
            Lessons = course.Lessons?.Select(lesson => GetLessonViewIdModel(lesson)).ToList()
        };
    }

    public static List<CourseViewIdModel> GetCourseViewIdModels(List<CourseEntityModel> courses)
    {
        return courses.Select(courseEM => 
            GetCourseViewIdModel(courseEM)
            ).ToList();
    }

    public static CourseEntityModel GetCourseEntityModel(CourseViewModel course)
    {
        return new CourseEntityModel
        {
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons?.Select(lesson => GetLessonEntityModel(lesson)).ToList()
        };
    }

    public static List<CourseEntityModel> GetCourseEntityModels(List<CourseViewModel> courses)
    {
        return courses.Select(courseEM => 
            GetCourseEntityModel(courseEM)
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
            GetUserViewModel(userEM)
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
            GetUserViewIdModel(userEM)
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
            GetUserEntityModel(userEM)
            ).ToList();
    }

}